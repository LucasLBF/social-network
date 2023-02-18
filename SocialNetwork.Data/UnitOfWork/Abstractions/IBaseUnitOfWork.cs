namespace SocialNetwork.Data.UnitOfWork.Abstractions
{
    public interface IBaseUnitOfWork
    {
        Task<int> SaveChanges();
        void BeginTransaction();
        void RollbackTransaction();
        void Commit();
    }
}
