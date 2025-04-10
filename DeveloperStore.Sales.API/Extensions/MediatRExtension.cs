using DeveloperStore.Sales.Application.Events;

namespace DeveloperStore.Sales.API.Extensions
{
    public static class MediatRExtension
    {
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(QueueMessageEvent).Assembly));

            return services;
        }
    }
}
