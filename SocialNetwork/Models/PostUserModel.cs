namespace SocialNetwork.API.Models
{
    public class PostUserModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
