
using System;
using System.Collections.Generic;
using Appapi.Models;
using FluentValidation;

namespace Appapi.Dtos.Employe
{
    public class CustomerAccordForAddDto
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string FourthName { get; set; }
        public string Family { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<CustomerTelephoneNumber> CustomerTelephoneNumbers { get; set; }
        public virtual ICollection<CustomerEmail> CustomerEmails { get; set; }
        public virtual ICollection<CustomerAttachment> CustomerAttachments { get; set; }

        public string Type { get; set; }
        public string SequenceNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReceiving { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public int CustomerId { get; set; }
        public virtual ICollection<CustomerAccordAttachment> CustomerAccordAttachments { get; set; }





        public CustomerAccordForAddDto()
        {
            Created = DateTime.Now;
            IsDelete = false;
            Gender = true;
        }

        public class CustomerAccordForAddDtoValidator : AbstractValidator<CustomerAccordForAddDto>
        {
            public CustomerAccordForAddDtoValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("Please The First Name Is Required");
                RuleFor(x => x.SecondName).NotEmpty().NotNull().WithMessage("Please The SecondName Is Required");
                RuleFor(x => x.ThirdName).NotEmpty().NotNull().WithMessage("Please The ThirdName Is Required");
                RuleFor(x => x.DateOfBirth).NotEmpty().NotNull();
                RuleFor(x => x.Gender).NotEmpty().NotNull();
                RuleFor(x => x.Created).NotEmpty().NotNull();
                RuleFor(x => x.Type).NotEmpty().NotNull().WithMessage("Please The Type Is Required");
                RuleFor(x => x.SequenceNumber).NotEmpty().NotNull();
            }
        }


    }
}