using DeveloperStore.Sales.API.Extensions;
using DeveloperStore.Sales.API.Settings;
using Serilog;

public class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.ConfigureLoggingExtensions();

            builder.Services.AddCustomIdentity(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCustomJwt(builder.Configuration);
            builder.Services.AddCustomSwagger();
            builder.Services.AddCustomHealthChecks(builder.Configuration);
            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCustomExceptionHandler();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/health");


            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}