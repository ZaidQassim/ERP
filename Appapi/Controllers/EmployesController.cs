using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appapi.Data;
using Appapi.Data.Interfaces;
using Appapi.Dtos.Employe;
using Appapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appapi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]

    public class EmployesController : ControllerBase
    {
        private readonly IUnitOfWork<Employe> _employe;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public EmployesController(IUnitOfWork<Employe> employe, IMapper mapper, DataContext context)
        {
            _employe = employe;
            _mapper = mapper;
            _context = context;
        }


        [HttpGet("getDetailsEmploye/{id}")]
        public async Task<IActionResult> GetDetailsEmploye(int id)
        {
            var employe = await _employe.Entity.GetById(id);
            if (employe == null)
                return NotFound("this item is not found");

            var employeToReturn = _mapper.Map<EmployeForDetailsDto>(employe);
            return StatusCode(200, new
            {
                code = 200,
                message = "getDetailsEmploye successfuly",
                result = new { employeToReturn }
            });
        }


        [HttpGet("getEmployes")]
        public async Task<IActionResult> GetEmployes(int? skip, int? take)
        {
            if (!skip.HasValue) skip = 0;
            if (!take.HasValue) take = 0;

            var count = await _employe.Entity.Count();
            var employes = await _employe.Entity.GetAll(skip.Value, take.Value);
            var employesToReturn = _mapper.Map<IEnumerable<EmployeForListDto>>(employes);
            return StatusCode(200, new
            {
                code = 200,
                message = "getEmployes successfuly",
                result = new { employesToReturn, count }
            });
        }

        [HttpGet("listEmployes")]
        public async Task<IActionResult> GetlistEmployes()
        {
            var listemployes = await _context.Employes.Select(e => new
            {
                e.Id,
                fullName = e.FirstName + " " + e.SecondName + " " + e.ThirdName + " " +  e.FourthName
            }).OrderBy(e => e.Id).ToListAsync();
            return StatusCode(200, new
            {
                code = 200,
                message = "listEmployes successfuly",
                result = new { listemployes }
            });
        }



        [HttpPost("addEmploye")]
        public async Task<IActionResult> AddEmploye([FromBody] EmployeForAddDto employeForAddDto)
        {
            var employeToCreate = _mapper.Map<Employe>(employeForAddDto);
            if (employeToCreate == null)
                return BadRequest();

            await _employe.Entity.Insert(employeToCreate);
            await _employe.Save();

            return StatusCode(200, new
            {
                code = 200,
                message = "add successfuly",
                result = new { employeToCreate.Id }
            });

        }


        [HttpDelete("deleteEmploye/{id}")]
        public async Task<IActionResult> DeleteEmploye([FromRoute] int id)
        {
            var employe = await _employe.Entity.GetById(id);
            if (employe == null)
                return NotFound("the item is not found");

            employe.IsDelete = true;
            await _employe.Save();

            return StatusCode(200, new
            {
                code = 200,
                message = "delete successfuly",
                result = new { employe.Id }
            });
        }




    }
}