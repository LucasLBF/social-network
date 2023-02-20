using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Repositories.Abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> GetUsersByNameAsync(string userName);
        Task<IEnumerable<User>> GetFollowers(int userId);
        Task<IEnumerable<User>> GetFollowing(int userId);
        Task<bool> CheckExistingEmail(string email);
    };
}
