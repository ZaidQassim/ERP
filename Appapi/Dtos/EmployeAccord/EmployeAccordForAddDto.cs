using System;
using System.Collections.Generic;
using Appapi.Dtos.EmployeAccord;
using Appapi.Models;
using FluentValidation;

namespace Appapi.Dtos.EmployeAccord
{
    public class EmployeAccordForAddDto
    {
        public string Type { get; set; }
        public string SequenceNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReceiving { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }
        public int EmployeId { get; set; }

        public virtual ICollection<EmployeAccordAttachmentForAddDto> EmployeAccordAttachments  { get; set; }


        public EmployeAccordForAddDto()
        {
            Created = DateTime.Now;
            DateOfReceiving = DateTime.Now;
            IsDelete  = false;
        }

        public class EmployeAccordForAddDtoValidator : AbstractValidator<EmployeAccordForAddDto>
        {
            public EmployeAccordForAddDtoValidator()
            {
                RuleFor(x => x.Type).NotEmpty().NotNull().WithMessage("Please The First Name Is Required");
                RuleFor(x => x.SequenceNumber).NotEmpty().NotNull().WithMessage("Please The First Name Is Required");
                RuleFor(x => x.DateOfReceiving).NotEmpty().NotNull().WithMessage("Please The First Name Is Required");
                RuleFor(x => x.EmployeId).NotEmpty().NotNull();
                RuleFor(x => x.Description).NotEmpty().NotNull();
                RuleFor(x => x.Created).NotEmpty().NotNull();
            }

        }

    }
}