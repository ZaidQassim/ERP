using System;
using System.Collections.Generic;
using Appapi.Dtos.Employe;

namespace Appapi.Dtos.EmployeAccord
{
    public class EmployeAccordForListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string SequenceNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateOfReceiving { get; set; }
        public DateTime DateOfDelivery { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Created { get; set; }
        public int EmployeId { get; set; }

    }
}