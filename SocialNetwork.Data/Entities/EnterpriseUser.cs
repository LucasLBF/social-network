using SocialNetwork.Data.Enums;

namespace SocialNetwork.Data.Entities
{
    public class EnterpriseUser
    {
        public int Id { get; set; }
        public EnterpriseCategory Category { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
