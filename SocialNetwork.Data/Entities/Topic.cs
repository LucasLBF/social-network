namespace SocialNetwork.Data.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = null!;

        public Topic(string name)
        {
            Name = name;
        }
    }
}
