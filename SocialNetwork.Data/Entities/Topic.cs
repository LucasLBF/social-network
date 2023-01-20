namespace SocialNetwork.Data.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<PostTopic>? Posts { get; set; }
    }
}
