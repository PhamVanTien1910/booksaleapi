using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookSale.Application.Services.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BookSale.Application.Services.CurrentUser;
using BookSale.Application.Services.Users;
using BookSale.Application.Services.Admin;
using BookSale.Application.Services.VNPayOrder;
using BookSale.Application.Services.Admin.Authors;
using BookSale.Application.Services.Admin.Categoris;
using BookSale.Application.Services.Admin.Book;
using BookSale.Application.Configuration;
using BookSale.Application.EmailHelper;
using dotnet_boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using BookSale.Application.Services.Carts;

namespace BookSale.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services, IConfiguration configuration)
        {

            // Add MVC services with support for views
            services.AddControllersWithViews();

            // Register HttpContextAccessor to access HTTP context
            services.AddHttpContextAccessor();

            // Register AutoMapper and specify the assembly to scan for profiles
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            // Register application services for dependency injection
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IVNPayOrderService, VNPayOrderService>();
            services.AddScoped<IAuthorsService, AuthorsService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<ICartService, CartService>();
            // Configure email settings from configuration
            services.Configure<EmailConfig>(options =>
            {
                configuration.GetSection("MailSetting").Bind(options);
            });

            // Configure JWT authentication
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtSettings:Secret").Value!);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false; // Allow non-HTTPS metadata for local testing
                x.SaveToken = true; // Save the token in the authentication context

                // Set token validation parameters
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), // Use the secret key for signing
                    ValidateIssuer = false, // Disable issuer validation
                    ValidateAudience = false, // Disable audience validation
                    NameClaimType = "user_id" // Map the claim for user identification
                };

                // Configure custom JWT bearer events
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Add a custom header if the token is expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers["Token-Expired"] = "true";
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            // Return the configured service collection
            return services;
        }
    }
}
