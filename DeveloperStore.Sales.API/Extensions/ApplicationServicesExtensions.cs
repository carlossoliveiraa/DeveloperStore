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
            services.AddDbContext<SalesDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SalesConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<SaleAppService>();
            services.AddScoped<ISaleEventPublisher, FakeSaleEventPublisher>();
        }
    }
}
