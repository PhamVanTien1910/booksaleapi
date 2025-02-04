
using BookSale.Application.Repositories;
using DotnetBoilerplate.Core.Configuration;
using DotnetBoilerplate.Core.EmailHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetBoilerplate.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailConfig>(options =>
            {
                configuration.GetSection("MailSetting").Bind(options);
            });
            services.AddScoped<IEmailHelper, DotnetBoilerplate.Core.EmailHelper.EmailHelper>();

            return services;
        }
    }
}
