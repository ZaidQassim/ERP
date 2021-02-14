using System;
using System.Collections.Generic;

namespace Appapi.Models
{
    public class CustomerAccord
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string SequenceNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReceiving { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customers { get; set; }

        public virtual ICollection<CustomerAccordAttachment> CustomerAccordAttachments { get; set; }

    }
}