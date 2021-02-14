namespace Appapi.Models
{
    public class EmployeAttachment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }

        public int EmployeId { get; set; }
        public virtual Employe Employes { get; set; }
    }
}