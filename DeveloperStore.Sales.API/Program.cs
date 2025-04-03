using DeveloperStore.Sales.API.Extensions;
using DeveloperStore.Sales.API.Settings;
using DeveloperStore.Sales.Infrastructure.Data.Context;
using DeveloperStore.Sales.Infrastructure.Data;
using DeveloperStore.Sales.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);

         
            builder.Host.ConfigureLoggingExtensions();

            var services = builder.Services;
            var configuration = builder.Configuration;

         
            services.AddCustomIdentity(configuration);
            services.AddApplicationServices(configuration);
            services.AddCustomJwt(configuration);
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            services.AddCustomSwagger();
            services.AddCustomHealthChecks(configuration);
            services.AddControllers();

          
            var app = builder.Build();

          
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeveloperStore Sales API v1");
                c.RoutePrefix = "swagger";
                c.DocumentTitle = "DeveloperStore API";
                c.DefaultModelsExpandDepth(-1);
                c.EnableFilter();
                c.EnableDeepLinking();
                c.DisplayRequestDuration();
                c.EnableValidator();
            });

          
            app.UseCustomExceptionHandler();

        
            app.UseSerilogRequestLogging();

     
            app.UseCors(policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader());

            app.UseHttpsRedirection();

       
            app.UseAuthentication();
            app.UseAuthorization();

          
            app.MapControllers();
            app.MapHealthChecks("/health");

            using (var scope = app.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                try
                {
                    var salesDbContext = scopedServices.GetRequiredService<SalesDbContext>();
                    await salesDbContext.Database.MigrateAsync();

                    var userDbContext = scopedServices.GetRequiredService<UserDbContext>();
                    await userDbContext.Database.MigrateAsync();

                    var userManager = scopedServices.GetRequiredService<UserManager<User>>();
                    await DbInitializer.SeedAsync(userManager);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred during migration or seeding");
                    throw;
                }
            }



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
