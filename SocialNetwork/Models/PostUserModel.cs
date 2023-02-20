using Microsoft.Data.SqlClient.Server;

namespace SocialNetwork.API.Models
{
    public class PostUserModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ProfileModel Profile { get; set; } = null!;
    }
}
