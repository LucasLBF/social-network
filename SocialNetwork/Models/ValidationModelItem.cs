namespace SocialNetwork.API.Models
{
    public class ValidationModelItem
    {
        public string PropertyName { get; set; }
        public List<string> Messages { get; set; } = new List<string>();

        public ValidationModelItem(string propertyName, string[] messages)
        {
            PropertyName = propertyName;
            Messages.AddRange(messages);
        }
    }
}
