using System;

namespace Appapi.Models
{
    public class CustomerTelephoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Created { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
    }
}