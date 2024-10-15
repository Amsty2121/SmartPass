using Microsoft.EntityFrameworkCore;
using SmartPass.Repository.Interfaces;
using SmartPass.Repository.Models.EntityInterfaces;
using System.Linq.Expressions;

namespace SmartPass.Repository.Implementations
{
    public class GenericRepository<TDBContext, T> : IGenericRepository<TDBContext, T>
        where T : class, IBaseEntity
        where TDBContext : DbContext
    {
        private readonly TDBContext _context;

        public GenericRepository(TDBContext context)
        {
            _context = context;
        }

        public Task<T?> GetById(Guid id, CancellationToken ct = default)
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id, ct);
        }

        public Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return _context.Set<T>().FirstOrDefaultAsync(predicate, ct);
        }

        public async Task<int> Add(T entity, CancellationToken ct = default)
        {
            await _context.Set<T>().AddAsync(entity, ct);
            return await _context.SaveChangesAsync(ct);
        }

        public async Task<int> Update(T entity, CancellationToken ct = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync(ct);
        }

        public async Task<int> Remove(T entity, CancellationToken ct = default)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync(ct);
        }
        public async Task<bool> IsIdPresent(Guid id, CancellationToken ct = default)
        {
            return await _context.Set<T>().CountAsync(x => x.Id == id) > 0;
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken ct = default)
        {
            return await _context.Set<T>().ToListAsync(ct);
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync(ct);
        }

        public Task<int> CountAll(CancellationToken ct = default)
        {
            return _context.Set<T>().CountAsync(ct);
        }

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            return _context.Set<T>().CountAsync(predicate, ct);
        }

        public async Task<T?> GetByIdWithInclude(Guid id, CancellationToken ct = default, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = IncludeProperties(ct, includeProperties);
            return await query.FirstOrDefaultAsync(entity => entity.Id == id, ct);
        }

        public async Task<IEnumerable<T>> GetAllWithInclude(CancellationToken ct = default, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = IncludeProperties(ct, includeProperties);

            return await entities.ToListAsync(ct);
        }

        public async Task<IEnumerable<T>> GetWhereWithInclude(Expression<Func<T, bool>> predicate, CancellationToken ct = default, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = IncludeProperties(ct, includeProperties);
            return await entities.Where(predicate).ToListAsync(ct);
        }

        private IQueryable<T> IncludeProperties(CancellationToken ct = default, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }

        /*public async Task<PaginatedResult<T>> GetPagedData<T>(PagedRequest pagedRequest, CancellationToken ct = default) where T : class, IBaseEntity
        {
            return await _context.Set<T>().CreatePaginatedResultAsync<T>(pagedRequest);
        }*/
    }
}
