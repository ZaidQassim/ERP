
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appapi.Data;
using Appapi.Data.Interfaces;
using Appapi.Dtos.EmployeAccord;
using Appapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appapi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]

    public class EmployeAccordsController : ControllerBase
    {
        private readonly IUnitOfWork<EmployeAccord> _employeAccord;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EmployeAccordsController(IUnitOfWork<EmployeAccord> employeAccord, IMapper mapper, DataContext context)
        {
            _employeAccord = employeAccord;
            _mapper = mapper;
            _context = context;
        }
        
        [HttpGet("getEmployeAccords")]
        public async Task<IActionResult> GetEmployeAccords(int? skip, int? take)
        {
            if (!skip.HasValue) skip = 0;
            if (!take.HasValue) take = 0;

            var count = await _employeAccord.Entity.Count();
            var employeAccords = await _employeAccord.Entity.GetAll(skip.Value, take.Value);
            // var employeAccordsToReturn = _mapper.Map<IEnumerable<EmployeAccordForListDto>>(employeAccords);
            var employeAccordsToReturn = employeAccords.Select(ea => new { 
                ea.Id,
                ea.Type,
                ea.SequenceNumber,
                ea.Description,
                ea.DateOfReceiving,
                ea.DateOfDelivery,
                attachments = _context.EmployeAccordAttachments.Where(a => a.EmployeAccordId == ea.Id).Count(),
                employe = _context.Employes.Where(e => e.Id == ea.EmployeId).Select(e => new {
                    e.FirstName,
                    e.SecondName,
                    e.ThirdName,
                    e.FourthName,
                    e.State,
                    telephoneNumber = _context.EmployeTelephoneNumbers.Where(t => t.EmployeId == e.Id)
                        .FirstOrDefault().Number
                }).FirstOrDefault(),
            }).ToList();

            return StatusCode(200, new
            {
                code = 200,
                message = "getEmployeAccord successfuly",
                result = new { employeAccordsToReturn, count }
            });
        }


        [HttpGet("getDetailsEmployeAccord/{id}")]
        public async Task<IActionResult> GetDetailsEmployeAccord(int id)
        {

            var employeAccord = await _employeAccord.Entity.GetById(id);
            if (employeAccord == null)
                return NotFound("this item is not found");

            var employeAccordToReturn = _mapper.Map<EmployeAccordForDetailsDto>(employeAccord);
            return StatusCode(200, new
            {
                code = 200,
                message = "getDetailsEmployeAccord successfuly",
                result = new { employeAccordToReturn }
            });
        }




        [HttpPost("addEmployeAccord")]
        public async Task<IActionResult> AddEmployeAccord([FromBody] EmployeAccordForAddDto employeAccordForAddDto)
        {
            var employeAccordToCreate = _mapper.Map<EmployeAccord>(employeAccordForAddDto);
            if (employeAccordToCreate == null)
                return BadRequest();

            await _employeAccord.Entity.Insert(employeAccordToCreate);
            await _employeAccord.Save();

            return StatusCode(200, new
            {
                code = 200,
                message = "addEmployeAccord successfuly",
                result = new { employeAccordToCreate.Id }
            });

        }


        [HttpDelete("deleteEmployeAccord/{id}")]
        public async Task<IActionResult> DeleteEmployeAccord([FromRoute] int id)
        {
            var employeAccord = await _employeAccord.Entity.GetById(id);
            if (employeAccord == null)
                return NotFound("the item is not found");

            employeAccord.IsDelete = true;
            await _employeAccord.Save();

            return StatusCode(200, new
            {
                code = 200,
                message = "delete successfuly",
                result = new { employeAccord.Id }
            });
        }








    }
}