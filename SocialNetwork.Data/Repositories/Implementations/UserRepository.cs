using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Context;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Repositories.Implementations
{
    public class UserRepository
    {
        private readonly SocialNetworkContext _context;
        public UserRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            User? user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<IEnumerable<User>?> GetUsersByNameAsync(string userName)
        {
            var users = await _context.Users?
                .Where(u => u.FirstName.Contains(userName) || u.LastName.Contains(userName))
                .ToListAsync()!;

            return users;
        }
    }
}
