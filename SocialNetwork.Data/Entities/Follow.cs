namespace SocialNetwork.Data.Entities
{
    public class Follow
    {
        public int FollowerId { get; set; }
        public virtual User Follower { get; set; } = null!;
        public int FollowedId { get; set; }
        public virtual User Followed { get; set; } = null!;
    }
}
