using System;
using System.Collections.Generic;
using Appapi.Models;

namespace Appapi.Dtos.CustomerAccord
{
    public class CustomerAccordForDetailsDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string SequenceNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReceiving { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }
        public virtual ICollection<CustomerAccordAttachmentForListDto> CustomerAccordAttachments { get; set; }
        public CustomersForListDto Customers { get; set; }
    

        // public ICollection<EmployeAttachmentForListDto> EmployeAttachments { get; set; }
        // public ICollection<EmployeEmailForListDto> EmployeEmails { get; set; }
        // public ICollection<EmployeTelephoneNumbersForListDto> EmployeTelephoneNumbers { get; set; }
    }
}