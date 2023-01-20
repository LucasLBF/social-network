using SocialNetwork.Data.Enums;

namespace SocialNetwork.Data.Entities
{
    public class Community : BaseEntity
    {
        public string? Title { get; set; }
        public Visibility Visibility { get; set; }
        public Guid CreatorId { get; set; }
        public User? Creator { get; set; }
        public ICollection<UserCommunity>? Users { get; set; }
    }
}
