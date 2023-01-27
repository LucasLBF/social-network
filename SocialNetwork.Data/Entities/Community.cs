using SocialNetwork.Data.Enums;

namespace SocialNetwork.Data.Entities
{
    public class Community : BaseEntity
    {
        public string Title { get; set; }
        public Visibility Visibility { get; set; }
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; } = null!;

        public Community(string title)
        {
            Title = title;
        }

    }
}
