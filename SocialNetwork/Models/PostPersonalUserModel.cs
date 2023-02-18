using SocialNetwork.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.API.Models
{
    public class PostPersonalUserModel : PostUserModel
    {
        private const int MINIMUM_AGE = 12;
        public Status Status { get; set; }
        [Required(ErrorMessage = "Genre is required")]
        public Genre? Genre { get; set; }
        [Required(ErrorMessage = "Age is required")]
        [Range(MINIMUM_AGE, int.MaxValue, ErrorMessage = "Age must be 12 or greater")]
        public int? Age { get; set; }
    }
}
