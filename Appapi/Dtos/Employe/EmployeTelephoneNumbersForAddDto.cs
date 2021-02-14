using System;

namespace Appapi.Dtos.Employe
{
    public class EmployeTelephoneNumbersForAddDto
    {
        public string Number { get; set; }
        public DateTime Created { get; set; }

        public EmployeTelephoneNumbersForAddDto()
        {
            Created = DateTime.Now;
        }

    }
}