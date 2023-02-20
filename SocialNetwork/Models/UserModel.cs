namespace SocialNetwork.API.Models
{
    public class UserModel
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ProfileModel Profile { get; set; } = null!;
    }
}
