using SocialNetwork.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models
{
    public class PostEnterpriseUserModel : PostUserModel
    {
        [Required(ErrorMessage = "Category is required")]
        public EnterpriseCategory Category { get; set; }
    }
}
