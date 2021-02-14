using System;

namespace Appapi.Models
{
    public class EmployePaymentCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string NumberCard { get; set; }
        public DateTime Created { get; set; }

        public int EmployeId { get; set; }
        public virtual Employe Employes { get; set; }

    }
}