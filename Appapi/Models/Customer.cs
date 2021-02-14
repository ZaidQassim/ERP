using System;
using System.Collections.Generic;

namespace Appapi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public string Family { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<CustomerAccord> CustomerAccords { get; set; }
        public virtual ICollection<CustomerTelephoneNumber> CustomerTelephoneNumbers { get; set; }
        public virtual ICollection<CustomerEmail> CustomerEmails { get; set; }
        public virtual ICollection<CustomerAttachment> CustomerAttachments { get; set; }

    }
}