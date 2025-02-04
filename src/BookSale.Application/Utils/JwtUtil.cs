using dotnet_boilerplate.Domain.Entities;
using BookSale.Domain.Payloads;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Sockets;

namespace BookSale.Application.Utils
{
    public class JwtUtil
    {
        private static JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
        private static SymmetricSecurityKey GetSymmetricSecurityKey(string secretKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }
        private static SecurityToken GennerateToken(User user, string secretKey, double expirationDays)
        {
            var key = GetSymmetricSecurityKey(secretKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var roleName = user.Role?.Name ?? "Unknown";
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("user_id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, roleName),
                    new Claim("email", user.Email)// Lấy vai trò từ Role.Name
                }),
                Expires = DateTime.UtcNow.AddDays(expirationDays),
                SigningCredentials = creds
            };
            return _tokenHandler.CreateToken(tokenDescriptor);
        }
        public static TokenPayLoad GennerateAccessToken(User user, IConfiguration configuration) 
        {
            var secretKey = configuration.GetSection("JwtSettings:Secret")?.Value ?? "secret";
            var expirationTime = double.Parse(configuration.GetSection("JwtSettings:AccessTokenExpirationTime")?.Value ?? "120");
            var accessToken = GennerateToken(user, secretKey, expirationTime);
            var token = new TokenPayLoad();
            token.Access = _tokenHandler.WriteToken(accessToken);
            return token;
        }
        public static TokenPayLoad GennerateAccessAndRefreshToken(User user, IConfiguration configuration)
        {
            var secretKey = configuration.GetSection("JwtSettings:Secret")?.Value ?? "secret";
            var accessTokenExpirationTime = double.Parse(configuration.GetSection("JwtSettings:AccessTokenExpirationTime")?.Value ?? "120");
            var refreshTokenExpirationTime = double.Parse(configuration.GetSection("JwtSettings:RefreshTokenExpirationTime")?.Value ?? "10080");
            var accessToken = GennerateToken(user, secretKey, accessTokenExpirationTime);
            var refreshToken = GennerateToken(user, secretKey, refreshTokenExpirationTime);
            var token = new TokenPayLoad
            {
                Access = _tokenHandler.WriteToken(accessToken),
                Refresh = _tokenHandler.WriteToken(refreshToken)
            };
            return token;
        }
    }
}
