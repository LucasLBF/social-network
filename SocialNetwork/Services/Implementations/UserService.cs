using SocialNetwork.API.Services.Abstractions;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.UnitOfWork.Abstractions;

namespace SocialNetwork.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User?> GetUser(int userId)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetFollowers(int userId)
        {
            return await _unitOfWork.UserRepository.GetFollowers(userId);
        }

        public async Task<IEnumerable<User>> GetFollowing(int userId)
        {
            return await _unitOfWork.UserRepository.GetFollowing(userId);
        }

        public async Task<IEnumerable<User>> GetUsersByName(string? name)
        {
            IEnumerable<User> users = new List<User>();

            if (String.IsNullOrEmpty(name))
            {
                return users;
            }

            users = await _unitOfWork.UserRepository.GetUsersByNameAsync(name);

            return users;
        }

        public async Task AddUser(User user, PersonalUser personalUser)
        {
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.PersonalUserRepository.AddAsync(personalUser);
            
            await _unitOfWork.SaveChanges();
        }

        public async Task AddUser(User user, EnterpriseUser enterpriseUser)
        {
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.EnterpriseUserRepository.AddAsync(enterpriseUser);
            
            await _unitOfWork.SaveChanges();
        }

        public async Task<bool> CheckIfExists(int id)
        {
            return await _userRepository.CheckIfExists(id);
        }
    }
}
