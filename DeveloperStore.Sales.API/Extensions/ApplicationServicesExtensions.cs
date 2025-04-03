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
            // Configure SalesDbContext (PostgreSQL)
            services.AddDbContext<SalesDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SalesConnection")));

            // Register Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register application services
            services.AddScoped<SaleAppService>();

            // Register event publisher (placeholder/mock)
            services.AddScoped<ISaleEventPublisher, FakeSaleEventPublisher>();
        }
    }
}
