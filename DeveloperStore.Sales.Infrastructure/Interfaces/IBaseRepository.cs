using DeveloperStore.Sales.CrossCutting.Pagination;
using DeveloperStore.Sales.Domain;
using System.Linq.Expressions;

namespace DeveloperStore.Sales.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<PaginatedList<T>> GetPagedAsync(Expression<Func<T, bool>> predicate, int page, int pageSize);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
