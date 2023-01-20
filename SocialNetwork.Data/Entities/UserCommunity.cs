namespace SocialNetwork.Data.Entities
{
    public class UserCommunity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid CommunityId { get; set; }
        public Community? Community { get; set; }
    }
}
