using DeveloperStore.Sales.Domain;
using DeveloperStore.Sales.Infrastructure.Data.Context;
using DeveloperStore.Sales.Infrastructure.Interfaces;
using DeveloperStore.Sales.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DeveloperStore.Sales.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesDbContext _context;
        private bool _disposed;
        private Hashtable? _repositories;

        public UnitOfWork(SalesDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<T> Repository<T>() where T : BaseEntity
        {
            _repositories ??= new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance!);
            }

            return (IBaseRepository<T>)_repositories[type]!;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Unchanged;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
                _context.Dispose();

            _disposed = true;
        }
    }
}
