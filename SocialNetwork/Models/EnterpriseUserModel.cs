using SocialNetwork.Data.Enums;

namespace SocialNetwork.API.Models
{
    public class EnterpriseUserModel : UserModel
    {
        public EnterpriseCategory Category { get; set; }
    }
}
