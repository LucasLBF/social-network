namespace SocialNetwork.Data.Entities
{
    public class Comment : Message
    {
        public Guid PostId { get; set; }
        public Post? Post { get; set; }
    }
}
