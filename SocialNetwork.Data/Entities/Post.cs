namespace SocialNetwork.Data.Entities
{
    public class Post : Message
    {
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostTopic>? Topics { get; set; }
    }
}
