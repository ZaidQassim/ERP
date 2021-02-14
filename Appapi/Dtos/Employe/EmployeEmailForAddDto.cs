using System;
using FluentValidation;

namespace Appapi.Dtos.Employe
{
    public class EmployeEmailForAddDto
    {
        public string Email { get; set; }
        public DateTime Created { get; set; }

       

        public EmployeEmailForAddDto()
        {
            Created = DateTime.Now;
        }

        public class EmployeEmailForAddDtoValidator : AbstractValidator<EmployeEmailForAddDto>
        {
            public EmployeEmailForAddDtoValidator()
            {
                RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            }

        }




    }
}