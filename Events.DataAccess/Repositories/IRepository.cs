using System.Linq.Expressions;

namespace Events.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task<T?> GetAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> UpdateAsync(T entity, Expression<Func<T, bool>> filter);

        Task<T?> DeleteAsync(Expression<Func<T, bool>> filter);
    }
}
