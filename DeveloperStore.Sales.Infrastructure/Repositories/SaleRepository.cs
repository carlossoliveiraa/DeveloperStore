using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Infrastructure.Data.Context;
using DeveloperStore.Sales.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Sales.Infrastructure.Repositories
{
    public sealed class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        private readonly SalesDbContext _context;

        public SaleRepository(SalesDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Sale?> GetByIdWithItemsAsync(Guid id)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
