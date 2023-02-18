using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Repositories.Abstractions;
using System.Linq.Expressions;

namespace SocialNetwork.Data.Repositories.Implementations
{
    public class PersonalUserRepository : BaseRepository<PersonalUser>, IPersonalUserRepository
    {
        public PersonalUserRepository(SocialNetworkContext context)
            : base(context) { }

        public async Task<IEnumerable<PersonalUser>> GetPersonalUserByUserId(int userId)
        {
            Expression<Func<PersonalUser, bool>> filter = pu => pu.UserId == userId;

            return await GenerateQuery(filter: filter).ToListAsync();
        }

        public async Task AddPersonalUser(PersonalUser user)
        {
            await base.AddAsync(user);
        }
    }
}
