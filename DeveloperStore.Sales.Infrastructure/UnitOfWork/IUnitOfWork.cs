using DeveloperStore.Sales.Domain;
using DeveloperStore.Sales.Infrastructure.Interfaces;

namespace DeveloperStore.Sales.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CommitAsync();
        void Rollback();
    }
}
