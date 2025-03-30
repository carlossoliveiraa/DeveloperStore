using DeveloperStore.Sales.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Sales.Infrastructure.TestUtils
{
    public static class TestSalesDbContextFactory
    {
        public static SalesDbContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<SalesDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new SalesDbContext(options);
        }
    }
}
