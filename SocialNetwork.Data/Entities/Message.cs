namespace SocialNetwork.Data.Entities
{
    public abstract class Message : BaseEntity
    {
        public string? Text { get; set; }
        public int Likes { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid? MediaId { get; set; }
        public Media? Media { get; set; }
    }
}
