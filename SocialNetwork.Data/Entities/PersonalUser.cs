using SocialNetwork.Data.Enums;

namespace SocialNetwork.Data.Entities
{
    public class PersonalUser
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public Genre Genre { get; set; }
        public int Age { get; set; }
        public Visibility Visibility { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
