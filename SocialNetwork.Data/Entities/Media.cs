using SocialNetwork.Data.Enums;
namespace SocialNetwork.Data.Entities
{
    public class Media
    {
        public int Id { get; set; }
        public MediaType MediaType { get; set; }
        public DateTime UploadedAt { get; set; }
        public string Url { get; set; }

        public Media(string url)
        {
            Url = url;
        }
    }
}
