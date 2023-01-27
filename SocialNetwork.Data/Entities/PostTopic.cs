namespace SocialNetwork.Data.Entities
{
    public class PostTopic
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; } = null!;
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; } = null!;
    }
}
