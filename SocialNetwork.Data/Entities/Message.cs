namespace SocialNetwork.Data.Entities
{
    public abstract class Message : BaseEntity
    {
        public string Text { get; set; }
        public int Likes { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
        public int? MediaId { get; set; }
        public virtual Media? Media { get; set; }

        public Message(string text)
        {
            Text = text;
        }
    }
}
