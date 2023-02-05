using System.Linq.Expressions;

namespace SocialNetwork.Data.Repositories.Implementations
{
    public abstract class QueryMethods<TEntity> where TEntity : class
    {
        protected abstract IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>>? filter = null,
                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                               params string[] includeProperties);
    }
}
