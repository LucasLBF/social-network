using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Repositories.Abstractions;
using System.Linq.Expressions;

namespace SocialNetwork.Data.Repositories.Implementations
{
    public class EnterpriseUserRepository : BaseRepository<EnterpriseUser>, IEnterpriseUserRepository
    {
        public EnterpriseUserRepository(SocialNetworkContext context)
            : base(context) { }

        public async Task<IEnumerable<EnterpriseUser>> GetEnterpriseUserByUserId(int userId)
        {
            Expression<Func<EnterpriseUser, bool>> filter = pu => pu.UserId == userId;

            return await GenerateQuery(filter: filter).ToListAsync();
        }

        public async Task AddEnterpriseUser(EnterpriseUser user)
        {
            await base.AddAsync(user);
        }
    }
}
