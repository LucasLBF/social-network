using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string? Email { get; set; }
        [MinLength(8), MaxLength(32)]
        public string Password { get; set; } = String.Empty;
        public Profile? Profile { get; set; }
        public ICollection<Follow>? Follows { get; set; }
        public ICollection<Follow>? FollowedBy { get; set; }
        public ICollection<UserCommunity>? Communities { get; set; }
        public ICollection<Community>? CreatedCommunities { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
