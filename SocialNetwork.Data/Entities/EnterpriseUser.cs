using SocialNetwork.Data.Enums;

namespace SocialNetwork.Data.Entities
{
    public class EnterpriseUser
    {
        public Guid Id { get; set; }
        public EnterpriseCategory Category { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
