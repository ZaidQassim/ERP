using System;

namespace Appapi.Models
{
    public class EmployeTelephoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Created { get; set; }

        public int EmployeId { get; set; }
        public virtual Employe Employes { get; set; }

    }
}