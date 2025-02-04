using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BookSale.Application.Repositories;
using BookSale.Infrastructure.Repositories;
using BookSale.Infrastructure.Migrations.Data;
using BookSale.Infrastructure.CommonService;
using dotnet_boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using BookSale.Application.Services.EmailSend;
using BookSale.Domain.Caching;
using BookSale.Infrastructure.Caching;
using StackExchange.Redis;


namespace BookSale.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                options.UseNpgsql(connectionString);
                //options.UseSqlServer(connectionString, sqlOptions =>
                //{
                //    sqlOptions.MigrationsAssembly(typeof(DataContext).Assembly.FullName);
                //});
            });

            // Register application services for dependency injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IOrderRepositoty, OrderRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IEmailTemplateRender, EmailTemplateRender>();
            services.AddScoped<ICacheService, RedisCacheService>();
            return services;
        }
    }
}
