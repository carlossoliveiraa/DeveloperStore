using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Sales.Infrastructure.Data.Context
{
    public static class SalesDbContextInMemory
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
