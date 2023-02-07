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

        public async Task<IEnumerable<User>> GetUsersByName(string? name)
        {
            IEnumerable<User> users = new List<User>();

            if (String.IsNullOrEmpty(name))
            {
                return users;
            }

            users = await _userRepository.GetUsersByNameAsync(name);

            return users;
        }

        public async Task<bool> CheckIfExists(int id)
        {
            return await _userRepository.CheckIfExists(id);
        }
    }
}
