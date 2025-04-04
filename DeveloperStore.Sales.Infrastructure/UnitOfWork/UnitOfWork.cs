using DeveloperStore.Sales.Domain;
using DeveloperStore.Sales.Infrastructure.Data.Context;
using DeveloperStore.Sales.Infrastructure.Interfaces;
using DeveloperStore.Sales.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace DeveloperStore.Sales.Infrastructure.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SalesDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories = new();
        private bool _disposed;

        public UnitOfWork(SalesDbContext context)
        {
            _context = context;
        }

        public IBaseRepository<T> Repository<T>() where T : BaseEntity
        {
            var typeName = typeof(T).Name;

            // Retorna do cache ou cria e armazena um novo repositório
            return (IBaseRepository<T>)_repositories.GetOrAdd(typeName, _ =>
            {
                var repositoryType = typeof(BaseRepository<>).MakeGenericType(typeof(T));
                return Activator.CreateInstance(repositoryType, _context)!;
            });
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

        private void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
