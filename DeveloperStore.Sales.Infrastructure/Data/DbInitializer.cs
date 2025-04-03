using DeveloperStore.Sales.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace DeveloperStore.Sales.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            var adminEmail = "admin@admin.com";
            var adminPassword = "admin";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var user = new User
                {
                    Name = "Administrator",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
