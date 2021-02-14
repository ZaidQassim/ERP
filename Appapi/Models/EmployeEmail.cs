using System;

namespace Appapi.Models
{
    public class EmployeEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }

        public int EmployeId { get; set; }
        public virtual Employe Employes { get; set; }
    }
}