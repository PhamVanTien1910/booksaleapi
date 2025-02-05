using BookSale.Api;
using BookSale.Api.Controllers.Auth;
using BookSale.Api.Filters;
using BookSale.Api.Middlewares;
using BookSale.Api.Validators;
using BookSale.Application.Dtos;
using BookSale.Application.Services.Auth;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Add validators from the current assembly
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Add validation filter globally
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });

        builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(10));

        // Add Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Sale Manager", Version = "v1" });

            // Add JWT Bearer authorization to Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
        });

        // Register Redis ConnectionMultiplexer
        var redisConnection = builder.Configuration.GetConnectionString("RedisConnection") ?? "localhost:6379";
        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));

        // Add application DI for services
        builder.Services.AddAppDI(builder.Configuration);

        builder.Services.AddHttpContextAccessor();
   

        var app = builder.Build();

        var logger = app.Services.GetRequiredService<ILogger<Program>>();

        try
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookSale.Infrastructure.Migrations.Data.DataContext>();

                logger.LogInformation("Testing database connection...");
                if (dbContext.Database.CanConnect())
                {
                    logger.LogInformation("Database connection successful.");
                    dbContext.Database.Migrate(); // auto update migrate
                }
                else
                {
                    logger.LogError("Failed to connect to the database.");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while testing the database connection.");
        }

        // Health check endpoint
        app.MapGet("/", async context =>
        {
            await context.Response.WriteAsync("Server live!");
        });
        

        app.UseCors(builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials();
        });

        // Use custom exception middleware
        app.UseMiddleware<ExceptionMiddlewares>();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(); 
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();  // Enforce HTTPS
        app.UseAuthentication();    // Enable authentication middleware
        app.UseAuthorization();     // Enable authorization middleware

        app.MapControllers();       // Map controller routes

        app.Run(); 

    }
}

      