using DeveloperStore.Sales.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Sales.Tests.IntegrationTests.Helpers
{
    public static class SalesDbContextFactory
    {
        public static SalesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SalesDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=SalesDb;Username=postgres;Password=postgres")
                .Options;

            var context = new SalesDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
