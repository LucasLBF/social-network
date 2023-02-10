namespace SocialNetwork.API.Models
{
    public class ValidationResponseModel
    {
        public ICollection<ValidationModelItem> Validations { get; set; } = new List<ValidationModelItem>();

        public bool HasError => Validations.Any();

        public void AddValidation(string propertyName, string message, params string[] messages)
        {
            List<string> errors = new List<string>();

            errors.Add(message);

            if (messages != null)
            {
                errors.AddRange(messages);
            }

            ValidationModelItem item = new ValidationModelItem(propertyName, errors.ToArray());
            Validations.Add(item);
        }
    }
}
