using System;

namespace Appapi.Dtos.Employe
{
    public class EmployeForListDto
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
        public int Age { get; set; }
        public DateTime DateOfStartWork { get; set; }
        public DateTime DateofSigningContract { get; set; }
        public bool Gender { get; set; }
        public int State { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }

        // EmployeAttachmentForListDto
    }
}