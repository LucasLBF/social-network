using SocialNetwork.Data.Enums;

namespace SocialNetwork.API.Models
{
    public class PersonalUserModel : UserModel
    {
        public Status Status { get; set; }
        public Genre Genre { get; set; }
        public int Age { get; set; }
    }
}
