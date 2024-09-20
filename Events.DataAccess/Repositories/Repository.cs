using Events.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Events.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter) => await _dbSet.Where(filter).FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> UpdateAsync(T entity, Expression<Func<T, bool>> filter)
        {
            var existingEntity = await GetAsync(filter);
            _dbSet.Update(entity);
            return existingEntity;
        }

        public async Task<T?> DeleteAsync(Expression<Func<T, bool>> filter)
        {
            var existingLiveEvent = await GetAsync(filter);
            _dbSet.Remove(existingLiveEvent);
            return existingLiveEvent;
        }

        public async Task<IEnumerable<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate) 
            => await _dbSet.Where(predicate).ToListAsync();
    }
}
