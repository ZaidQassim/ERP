namespace Appapi.Models
{
    public class CustomerAccordAttachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }

        public int CustomerAccordId { get; set; }
        public virtual CustomerAccord CustomerAccord { get; set; }
    }
}