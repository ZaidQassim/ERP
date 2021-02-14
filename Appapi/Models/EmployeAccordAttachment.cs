namespace Appapi.Models
{
    public class EmployeAccordAttachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }

        public int EmployeAccordId { get; set; }
        public virtual EmployeAccord EmployeAccords { get; set; }
    }
}