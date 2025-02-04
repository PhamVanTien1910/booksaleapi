using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using BookSale.Domain.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Auth
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(CancellationToken cancellationToken, RegistrationDto registrationDto);
        Task<TokenPayLoad> LoginAsync(TokenObtainPairDto tokenObtainPairDto);
        Task<bool> ConfirmEmailAsync(int userId, string token);
        Task SendPasswordResetEmail(CancellationToken cancellationToken,ResetEmailRequest email);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest resetPassword);
    }
}
