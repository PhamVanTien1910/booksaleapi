using AutoMapper;
using BCrypt.Net;
using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using BookSale.Application.Exceptions;
using BookSale.Application.Repositories;
using BookSale.Domain.Enums;
using BookSale.Domain.Payloads;
using BookSale.Domain.Specifications;
using BookSale.Domain.Specifications.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BookSale.Application.Utils;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using System.Web;
using BookSale.Application.EmailHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net.Http;
using BookSale.Application.Services.EmailSend;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Security.Policy;
using Microsoft.Extensions.Hosting;
using BookSale.Application.Dtos.Request;

namespace BookSale.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEmailHelper _emailHelper;
        private readonly IEmailTemplateRender _emailTemplateRender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, 
                           IUserRepository userRepository, IMapper mapper, IEmailHelper emailHelper, 
                            IEmailTemplateRender emailTemplateRender,
                            IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _emailHelper = emailHelper;
            _emailTemplateRender = emailTemplateRender;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
        }

        public async Task<TokenPayLoad> LoginAsync(TokenObtainPairDto tokenObtainPairDto)
        {
            var user = await _userRepository.FristOrDefaultAsync(
                spec: new UserEmailSpecification(tokenObtainPairDto.Email!),
                includes: new List<Expression<Func<User, object>>> { u => u.Role}
             );
            if (user == null || !BCrypt.Net.BCrypt.Verify(tokenObtainPairDto.Password, user.Password)) 
            {
                throw new CustomExceptions(StatusCodes.Status401Unauthorized, ErrorCodeEnums.IncorrectEmailOrPassword, "Incorrect Email Or Password");
            }
            if (user.IsEmailConfirmed == false)
            {
                throw new CustomExceptions(StatusCodes.Status401Unauthorized, ErrorCodeEnums.IncorrectEmailOrPassword, "Your email has not been confirmed. Please check your inbox and verify your email address.");
            }
            var tokenPayload = JwtUtil.GennerateAccessToken(user, _configuration);
            return tokenPayload;
        }

        public async Task<UserDto> RegisterAsync(CancellationToken cancellationToken,RegistrationDto registrationDto)
        {
            if (await _userRepository.ExistsAsync(new UserEmailSpecification(registrationDto.Email!))) 
            {
                throw new EmailExistedExceptions();
            }
            var user = _mapper.Map<User>(registrationDto);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            user.IsActive = false;
            user.IsEmailConfirmed = false;
            user.IsSuperUser = false;
            user.IsStaff = false;
            user.RoleId = (int)RoleEnums.Member;
            await _userRepository.AddAsync(user);
            var register = await _unitOfWork.SaveChangesAsync();

            string token =  GenerateEmailConfirmationToken(user);
            var actionContext = new ActionContext
            {
                HttpContext = _httpContextAccessor.HttpContext,
                RouteData = new Microsoft.AspNetCore.Routing.RouteData(), 
                ActionDescriptor = new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor() 
            };

            var urlHelper = _urlHelperFactory.GetUrlHelper(actionContext);
            var url = urlHelper.Action("ConfirmEmail", "Auth", new { memberKey = user.Id, token = token, method = "Get"}, _httpContextAccessor.HttpContext.Request.Scheme);
            string body = await _emailTemplateRender.GetTemplate("Template\\ConfirmEmail.html");
            body = string.Format(body, user.FullName, url);
            if(register > 0)
            {
                await _emailHelper.SendEmail(cancellationToken, new Dtos.Request.EmailRequest
                {
                    To = user.Email,
                    Subject = "Confirm Email For Register",
                    Content = body
                });
            }
            return _mapper.Map<UserDto>(user);
        }


        public async Task<bool> ConfirmEmailAsync(int userId, string token)
        {
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserIdNotFoundException();
            if (!ValidateEmailConfirmationToken(user, token))
            {
                throw new InvalidTokenException("Invalid or expired token");
            }
            user.IsEmailConfirmed = true;
            user.IsActive = true;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task SendPasswordResetEmail(CancellationToken cancellationToken ,ResetEmailRequest email)
        {
            var user = await _userRepository.FristOrDefaultAsync(new UserEmailSpecification(email.Email)) ?? throw new UserEmailNotFoundException();
            var token = GeneratePasswordResetToken(user);
            var resetLink = $"http://localhost:8080/reset-password?email={email}&token={token}";
            var body = $"Chào bạn, \n\nVui lòng nhấp vào liên kết dưới đây để đặt lại mật khẩu của bạn: \n{resetLink}\n\nNếu bạn không yêu cầu thay đổi mật khẩu, vui lòng bỏ qua email này.";
            await _emailHelper.SendEmail(cancellationToken, new Dtos.Request.EmailRequest
            {
                To = user.Email,
                Subject = "Password Reset Request",
                Content = body
            });
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest resetPassword)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(resetPassword.token);
            var userId = int.Parse(jwtToken.Claims.First(c => c.Type == "user_id").Value);
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new UserIdNotFoundException();
            if (!ValidateEmailConfirmationToken(user, resetPassword.token))
            {
                throw new InvalidTokenException("Invalid or expired token");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(resetPassword.newPassword);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        private string GenerateEmailConfirmationToken(User user)
        {
            var tokenPayload = JwtUtil.GennerateAccessToken(user, _configuration);
            var accessToken = tokenPayload.Access;
            return accessToken;
        }
        private string GeneratePasswordResetToken(User user)
        {
            var tokenPayload = JwtUtil.GennerateAccessToken(user, _configuration);
            return tokenPayload.Access;
        }

        private bool ValidateEmailConfirmationToken(User user, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Secret")?.Value ?? "secret");
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                var userIdClaim = principal.FindFirst("user_id");
                if (userIdClaim == null || userIdClaim.Value != user.Id.ToString())
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
