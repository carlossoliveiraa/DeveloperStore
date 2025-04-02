using DeveloperStore.Sales.Application.Interfaces.Messaging;
using DeveloperStore.Sales.Application.Services.Messaging;
using DeveloperStore.Sales.Application.Services;
using DeveloperStore.Sales.Infrastructure.UnitOfWork;
using DeveloperStore.Sales.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Sales.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext for Sales (PostgreSQL)
            services.AddDbContext<SalesDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SalesConnection")));

            // Unit of Work pattern
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Application Layer Services
            services.AddScoped<SaleAppService>();

            // Event Publisher (fake/mock for now)
            services.AddScoped<ISaleEventPublisher, FakeSaleEventPublisher>();
        }
    }
}
