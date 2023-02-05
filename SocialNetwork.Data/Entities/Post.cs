namespace SocialNetwork.Data.Entities
{
    public class Post : Message
    {
        public virtual ICollection<Comment> Comments { get; set; } = null!;
        public virtual ICollection<Topic> Topics { get; set; } = null!;

        public Post(string text)
            : base(text) { }
    }
}
