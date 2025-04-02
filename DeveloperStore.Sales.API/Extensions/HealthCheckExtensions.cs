namespace DeveloperStore.Sales.API.Extensions
{
    public static class HealthCheckExtensions
    {
        public static void AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks().AddNpgSql(configuration.GetConnectionString("SalesConnection")!);
        }
    }
}
