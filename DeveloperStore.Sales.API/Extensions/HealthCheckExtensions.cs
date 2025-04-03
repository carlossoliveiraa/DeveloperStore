namespace DeveloperStore.Sales.API.Extensions
{
    public static class HealthCheckExtensions
    {
        public static void AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            // Adds PostgreSQL health check using the configured connection string
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("SalesConnection")!);
        }
    }
}
