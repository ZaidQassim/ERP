using System;
using System.Collections.Generic;

namespace Appapi.Models
{
    public class Employe
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public string MotherFname { get; set; }
        public string MotherSname { get; set; }
        public string MotherTname { get; set; }
        public string Family { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfStartWork { get; set; }
        public DateTime DateofSigningContract { get; set; }
        public string Gender { get; set; }
        public int State { get; set; }
        public string StateMessage { get; set; }
        public DateTime DateofState { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }


        public virtual ICollection<EmployeAccord> EmployeAccords { get; set; }
        public virtual ICollection<EmployeAccordAttachment> EmployeAccordAttachments { get; set; }
        public virtual ICollection<EmployeAttachment> EmployeAttachments { get; set; }
        public virtual ICollection<EmployeEmail> EmployeEmails { get; set; }
        public virtual ICollection<EmployePaymentCard> EmployePaymentCards { get; set; }
        public virtual ICollection<EmployeTelephoneNumber> EmployeTelephoneNumbers { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

    }
}