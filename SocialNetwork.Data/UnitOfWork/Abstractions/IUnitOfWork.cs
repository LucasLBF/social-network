using SocialNetwork.Data.Repositories.Abstractions;

namespace SocialNetwork.Data.UnitOfWork.Abstractions
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IPersonalUserRepository PersonalUserRepository { get; }
        IEnterpriseUserRepository EnterpriseUserRepository { get; }
    }
}
