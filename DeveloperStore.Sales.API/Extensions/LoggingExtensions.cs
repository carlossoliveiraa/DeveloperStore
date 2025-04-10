using Serilog;

namespace DeveloperStore.Sales.API.Extensions
{
    public static class LoggingExtensions
    {
        public static void ConfigureLoggingExtensions(this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);                               
                config.WriteTo.Console();
                config.WriteTo.File("logs/api-log.txt", rollingInterval: RollingInterval.Day);
            });
        }
    }
}
