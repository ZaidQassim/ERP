using System;

namespace Appapi.Dtos.CustomerAccord
{
    public class CustomersForListDto
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
    }
}