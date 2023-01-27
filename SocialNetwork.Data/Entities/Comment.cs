namespace SocialNetwork.Data.Entities
{
    public class Comment : Message
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; } = null!;

        public Comment(string text)
            : base(text) { }
    }
}
