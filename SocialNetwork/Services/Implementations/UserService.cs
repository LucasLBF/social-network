using SocialNetwork.API.Services.Abstractions;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Repositories.Abstractions;

namespace SocialNetwork.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUser(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetFollowers(int userId)
        {
            return await _userRepository.GetFollowers(userId);
        }

        public async Task<IEnumerable<User>> GetFollowing(int userId)
        {
            return await _userRepository.GetFollowing(userId);
        }
    }
}
