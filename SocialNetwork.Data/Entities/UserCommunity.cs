namespace SocialNetwork.Data.Entities
{
    public class UserCommunity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public int CommunityId { get; set; }
        public virtual Community Community { get; set; } = null!;
    }
}
