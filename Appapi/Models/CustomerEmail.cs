using System;

namespace Appapi.Models
{
    public class CustomerEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customers { get; set; }
    }
}