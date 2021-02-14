using System;
using System.Collections.Generic;

namespace Appapi.Dtos.Employe
{
    public class EmployeForDetailsDto
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
        public bool Gender { get; set; }
        public int State { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }

        public ICollection<EmployeAttachmentForListDto> EmployeAttachments { get; set; }
        public ICollection<EmployeEmailForListDto> EmployeEmails { get; set; }
        public ICollection<EmployeTelephoneNumbersForListDto> EmployeTelephoneNumbers { get; set; }
        public ICollection<EmployePaymentCardForListDto> EmployePaymentCards { get; set; }
        

    }
}