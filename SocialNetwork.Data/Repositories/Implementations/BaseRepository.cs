using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialNetwork.Data.Repositories.Abstractions;
using System.Linq.Expressions;

namespace SocialNetwork.Data.Repositories.Implementations
{
    public abstract class BaseRepository<TEntity> : QueryMethods<TEntity>, IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public async virtual Task<bool> CheckIfExists(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id) != null;
        }

        public async virtual Task<TEntity> AddAsync(TEntity entity)
        {
            EntityEntry result = await _context.AddAsync(entity);
            return (TEntity)result.Entity;
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual void UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        protected override IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params string[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = GenerateWhereQueryable(query, filter);
            query = GenerateIncludeQueryable(query, includeProperties);
            query = GenerateOrderedQueryable(query, orderBy);

            return query;
        }

        #region Private Methods
        private IQueryable<TEntity> GenerateWhereQueryable(IQueryable<TEntity> query, Expression<Func<TEntity, bool>>? filter)
        {
            if (filter != null)
            {
                return query.Where(filter);
            }

            return query;
        }

        private IQueryable<TEntity> GenerateIncludeQueryable(IQueryable<TEntity> query,
            params string[] includeProperties)
        {
            foreach (string property in includeProperties)
            {
                query = query.Include(property);
            }
            return query;
        }

        private IQueryable<TEntity> GenerateOrderedQueryable(IQueryable<TEntity> query,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy)
        {
            if (orderBy != null)
            {
                query = orderBy(query);

            }
            return query;
        }
        #endregion
    }
}
