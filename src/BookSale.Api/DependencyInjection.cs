using BookSale.Infrastructure;
namespace BookSale.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI(configuration)
                    .AddInfrastructureDI(configuration);
            return services;
        }
    }
}
