using DeveloperStore.Sales.Infrastructure.Data.Context;
using DeveloperStore.Sales.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Sales.API.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure ASP.NET Identity with custom password and lockout settings
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
            
        }
    }
}
