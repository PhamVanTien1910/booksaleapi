using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using BookSale.Application.EmailHelper;
using BookSale.Application.Services.Auth;
using BookSale.Domain.Payloads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BookSale.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailHelper _emailHelper;
        public AuthController(IAuthService authService, IEmailHelper emailHelper)
        {
            _authService = authService;
            _emailHelper = emailHelper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenObtainPairDto tokenObtainPairDto)
        {
            var tokenPayload = await _authService.LoginAsync(tokenObtainPairDto);
            return Ok(tokenPayload);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(CancellationToken cancellationToken, [FromBody] RegistrationDto payload)
        {
            var userDto = await _authService.RegisterAsync(cancellationToken,payload);
            return StatusCode(201, userDto);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(int memberKey, string token)
        {
            await _authService.ConfirmEmailAsync(memberKey, token);
            return Ok(new { success = true, message = "Email confirmed successfully! You can now log in." });

        }

        [HttpPost("send-reset-email")]
        public async Task<IActionResult> SendPasswordResetEmail([FromBody] ResetEmailRequest email, CancellationToken cancellationToken)
        {
            await _authService.SendPasswordResetEmail(cancellationToken, email);

            return Ok(new { success = true, message = "Password reset email sent successfully!" });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPassword)
        {
            await _authService.ResetPasswordAsync(resetPassword);
            return Ok(new { success = true, message = "Password reset successfully" });
        }
    }
}
