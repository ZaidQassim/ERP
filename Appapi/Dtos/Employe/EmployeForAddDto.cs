using System;
using System.Collections.Generic;
using Appapi.Models;
using FluentValidation;

namespace Appapi.Dtos.Employe
{
    public class EmployeForAddDto
    {
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

        public virtual ICollection<EmployeAttachmentForAddDto> EmployeAttachments { get; set; }
        public virtual ICollection<EmployeEmailForAddDto> EmployeEmails { get; set; }
        public virtual ICollection<EmployeTelephoneNumbersForAddDto> EmployeTelephoneNumbers { get; set; }
        public virtual ICollection<EmployePaymentCardForAddDto> EmployePaymentCards { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }


        public EmployeForAddDto()
        {
            Created = DateTime.Now;
            IsDelete = false;
        }

        public class EmployeForAddDtoValidator : AbstractValidator<EmployeForAddDto>
        {
            public EmployeForAddDtoValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("Please The First Name Is Required");
                RuleFor(x => x.SecondName).NotEmpty().NotNull().WithMessage("Please The First Name Is Required");
                RuleFor(x => x.ThirdName).NotEmpty().NotNull().WithMessage("Please The First Name Is Required");
                RuleFor(x => x.DateOfBirth).NotEmpty().NotNull();
                RuleFor(x => x.DateOfStartWork).NotEmpty().NotNull();
                RuleFor(x => x.Gender).NotEmpty().NotNull();
                RuleFor(x => x.Created).NotEmpty().NotNull();
                // RuleFor(x => x.StateMessage).NotEmpty().When(z => z.State == 2);
            }
        }

       
    }
}