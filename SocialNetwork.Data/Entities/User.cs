using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Profile? Profile { get; set; }
        public ICollection<Follow>? Follows { get; set; }
        public ICollection<Follow>? FollowedBy { get; set; }
        public ICollection<UserCommunity>? Communities { get; set; }
        public ICollection<Community>? CreatedCommunities { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
