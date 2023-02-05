namespace SocialNetwork.Data.Repositories.Abstractions
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
    }
}
