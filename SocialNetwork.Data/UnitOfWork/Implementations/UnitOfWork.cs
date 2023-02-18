using SocialNetwork.Data.Context;
using SocialNetwork.Data.Repositories.Abstractions;
using SocialNetwork.Data.UnitOfWork.Abstractions;

namespace SocialNetwork.Data.UnitOfWork.Implementations
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork 
    {
        public IUserRepository UserRepository { get; }
        public IPersonalUserRepository PersonalUserRepository { get; }
        public IEnterpriseUserRepository EnterpriseUserRepository { get; }

        public UnitOfWork(SocialNetworkContext context,
            IUserRepository userRepository,
            IPersonalUserRepository personalUserRepository,
            IEnterpriseUserRepository enterpriseUserRepository) : base(context)
        {
            UserRepository = userRepository;
            PersonalUserRepository = personalUserRepository;
            EnterpriseUserRepository = enterpriseUserRepository;
        }
    }
}
