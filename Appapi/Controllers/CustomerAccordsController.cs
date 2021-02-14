


using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appapi.Data;
using Appapi.Data.Interfaces;
using Appapi.Dtos.CustomerAccord;
using Appapi.Dtos.Employe;
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

    public class CustomerAccordsController : ControllerBase
    {
        private readonly IUnitOfWork<CustomerAccord> _customerAccord;
        private readonly IUnitOfWork<Customer> _customer;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CustomerAccordsController(IUnitOfWork<Customer> customer, IUnitOfWork<CustomerAccord> customerAccord, IMapper mapper, DataContext context)
        {
            _customerAccord = customerAccord;
            _customer = customer;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("getCustomers")]
        public async Task<IActionResult> GetCustomers(int? skip, int? take)
        {
            if (!skip.HasValue) skip = 0;
            if (!take.HasValue) take = 0;

            var count = await _customer.Entity.Count();
            var customers = await _customer.Entity.GetAll(skip.Value, take.Value);
            var customersToReturn = _mapper.Map<IEnumerable<CustomersForListDto>>(customers);
            return StatusCode(200, new
            {
                code = 200,
                message = "getEmployeAccord successfuly",
                result = new { customersToReturn, count }
            });
        }


        [HttpGet("getDetailsCustomerAccord/{id}")]
        public async Task<IActionResult> GetDetailsCustomerAccord(int id)
        {

            var customerAccord = await _customerAccord.Entity.GetById(id);
            if (customerAccord == null)
                return NotFound("this item is not found");

            var customerAccordToReturn = _mapper.Map<CustomerAccordForDetailsDto>(customerAccord);
            return StatusCode(200, new
            {
                code = 200,
                message = "getDetailsEmployeAccord successfuly",
                result = new { customerAccordToReturn }
            });
        }




        [HttpPost("addCustomerAccord")]
        public async Task<IActionResult> AddCustomerAccord([FromBody] CustomerAccordForAddDto customerAccordForAddDto)
        {
            var customerToCreate = _mapper.Map<Customer>(customerAccordForAddDto);
            if (customerToCreate == null)
                return BadRequest();
            await _customer.Entity.Insert(customerToCreate);
            await _customer.Save();

            customerAccordForAddDto.CustomerId = customerToCreate.Id;
            var customerAccordToCreate = _mapper.Map<CustomerAccord>(customerAccordForAddDto);
            if (customerAccordToCreate == null)
                return BadRequest();

            await _customerAccord.Entity.Insert(customerAccordToCreate);
            await _customerAccord.Save();

            return StatusCode(200, new
            {
                code = 200,
                message = "addCustomerAccord successfuly",
                result = new { customerId = customerToCreate.Id, customerAccordId = customerAccordToCreate.Id }
            });

        }


        [HttpDelete("deleteCustomerAccord/{id}")]
        public async Task<IActionResult> DeleteCustomerAccord([FromRoute] int id)
        {
            var customer = await _customer.Entity.GetById(id);
            if (customer == null)
                return NotFound("the item is not found");

            customer.IsDelete = true;
            await _customer.Save();

            return StatusCode(200, new
            {
                code = 200,
                message = "deleteCustomerAccord successfuly",
                result = new { customer.Id }
            });
        }








    }
}