using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Repositories.Abstractions;
using System.Linq.Expressions;

namespace SocialNetwork.Data.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SocialNetworkContext context)
            : base(context) { }

        public async Task<IEnumerable<User>> GetUsersByNameAsync(string userName)
        {
            Expression<Func<User, bool>> filter = u => u.FirstName.Contains(userName) || u.LastName.Contains(userName);
                return await GenerateQuery(filter: filter)
                .ToListAsync();
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

        public async Task<bool> CheckExistingEmail(string email)
        {
            return await _context.Set<User>().AnyAsync(u => u.Email == email);
        }
    }
}
