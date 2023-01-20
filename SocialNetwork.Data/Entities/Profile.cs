namespace SocialNetwork.Data.Entities
{
    public class Profile : BaseEntity
    {
        public string? Description { get; set; }
        public int Likes { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
