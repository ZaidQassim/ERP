namespace Appapi.Models
{
    public class CustomerAttachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
    }
}