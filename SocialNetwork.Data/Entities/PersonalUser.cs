using SocialNetwork.Data.Enums;

namespace SocialNetwork.Data.Entities
{
    public class PersonalUser
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public Genre Genre { get; set; }
        public int Age { get; set; }
        public Visibility Visibility { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
