using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Repositories.Abstractions;

namespace SocialNetwork.Data.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SocialNetworkContext context)
            : base(context) { }

        public async Task<IEnumerable<User>> GetUsersByNameAsync(string userName)
        {
            IEnumerable<User> users =
                users = await GenerateQuery(filter: u => u.FirstName.Contains(userName) || u.LastName.Contains(userName))
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> GetFollowers(int userId)
        {
            User? userWithFollowers = await GenerateQuery(includeProperties: "Followers")
                .FirstOrDefaultAsync(u => u.Id == userId);

            IEnumerable<User> followers = new List<User>();

            if (userWithFollowers != null)
            {
                followers = userWithFollowers.Followers;
            }

            return followers;
        }
        public async Task<IEnumerable<User>> GetFollowing(int userId)
        {
            User? userWithFollowers = await GenerateQuery(includeProperties: "Following")
                .FirstOrDefaultAsync(u => u.Id == userId);

            IEnumerable<User> followers = new List<User>();

            if (userWithFollowers != null)
            {
                followers = userWithFollowers.Following;
            }

            return followers;
        }

    }
}
