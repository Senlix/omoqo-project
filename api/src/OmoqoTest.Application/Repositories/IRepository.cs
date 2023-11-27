using System.Linq.Expressions;
using OmoqoTest.Application.Extensions;
using OmoqoTest.Domain.Common;

namespace OmoqoTest.Application.Repositories
{
    public interface IRepository<T>
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PaginatedList<T>> GetAllAsync(
            int page = 0,
            int limit = 50,
            Expression<Func<T, bool>> filter = null!,
            List<OrderByExpression<T>> orderBy = null!);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}