using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Data.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [MinLength(8), MaxLength(32)]
        public string Password { get; set; }
        public virtual Profile Profile { get; set; } = null!;
        public virtual ICollection<User> Followers { get; set; } = null!;
        public virtual ICollection<User> Following { get; set; } = null!;
        public virtual ICollection<Community> Communities { get; set; } = null!;
        public virtual ICollection<Community> CreatedCommunities { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = null!;

        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    }
}
