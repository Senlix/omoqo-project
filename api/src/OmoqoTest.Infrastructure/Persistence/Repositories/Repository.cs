using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OmoqoTest.Application.Extensions;
using OmoqoTest.Application.Repositories;
using OmoqoTest.Domain.Common;

namespace OmoqoTest.Infrastructure.Persistence.Repositories
{
    public class Repository<T>(DbContext dbContext) : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<PaginatedList<T>> GetAllAsync(
            int page = 0,
            int limit = 50,
            Expression<Func<T, bool>> filter = null!,
            List<OrderByExpression<T>> orderBy = null!)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            int count = query.Count();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = query.OrderByDynamic(orderBy);
            }

            var items = await query.Skip((page - 1) * limit)
                                          .Take(limit)
                                          .ToListAsync();

            return new PaginatedList<T>(items, count, page, limit)
            ;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}