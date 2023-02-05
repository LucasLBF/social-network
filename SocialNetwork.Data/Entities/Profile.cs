namespace SocialNetwork.Data.Entities
{
    public class Profile : BaseEntity
    {
        public string Description { get; set; }
        public int Likes { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        public Profile(string description)
        {
            Description = description;
        }
    }
}
