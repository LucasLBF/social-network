using SocialNetwork.Data.Entities;

namespace SocialNetwork.API.Services.Abstractions
{
    public interface IUserService
    {
        Task<User?> GetUser(int userId);
        Task<IEnumerable<User>> GetFollowers(int userId);
        Task<IEnumerable<User>> GetFollowing(int userId);
        Task<IEnumerable<User>> GetUsersByName(string? name);
        Task<bool> CheckIfExists(int id);
    }
}
