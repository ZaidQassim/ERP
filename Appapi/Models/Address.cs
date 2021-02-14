using System;

namespace Appapi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public DateTime Created { get; set; }

        public int EmployeId { get; set; }
        public virtual Employe Employes { get; set; }

    }
}