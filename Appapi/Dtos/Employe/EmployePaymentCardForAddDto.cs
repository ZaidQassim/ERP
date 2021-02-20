using System;
using FluentValidation;

namespace Appapi.Dtos.Employe
{
    public class EmployePaymentCardForAddDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string NumberCard { get; set; }
        public string Code { get; set; }
        public DateTime DateExpired { get; set; }
        public DateTime Created { get; set; }

        public EmployePaymentCardForAddDto()
        {
            Created = DateTime.Now;
        }

        public class EmployePaymentCardForAddDtoValidator : AbstractValidator<EmployePaymentCardForAddDto>
        {
            public EmployePaymentCardForAddDtoValidator()
            {
                RuleFor(x => x.Type).NotEmpty().NotNull().WithMessage("Please The First Name Is Required");
                RuleFor(x => x.NumberCard).NotEmpty().NotNull()
                    .WithMessage("Please The First Name Is Required").CreditCard();
                RuleFor(x => x.Created).NotEmpty().NotNull();
            }

        }



    }
}