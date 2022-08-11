using AssetsManager.Models;
using AssetsManager.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AssetsManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
         private readonly IEmployeeDetailsRepository _employeeDetilsRepository;
        public EmployeesController(IEmployeeDetailsRepository employeeDetilsRepository)
        {
            _employeeDetilsRepository = employeeDetilsRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllEmployeeDetils() 
        {
            var employees = await _employeeDetilsRepository.GetAllEmployeeDetailsAsync();
            return Ok(employees);
        }

        [HttpGet("{Employeeid}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute]int Employeeid)
        {
            var employee = await _employeeDetilsRepository.GetEmployeeByIdAsync(Employeeid);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewEmployee([FromBody] EmployeeDetail employee)
        {
            var id = await _employeeDetilsRepository.AddEmployee(employee);
            employee.EmployeeId = id;
            return Ok(employee);
            //return 201 status code
            //return CreatedAtAction(nameof(GetEmployeeById),new { id=id,Controller="Employees"},id);
        }

        [HttpPut("{employeeid}")]
        public async Task<IActionResult> UpdateEmployeeDetails([FromBody] EmployeeDetail employee,[FromRoute]int employeeid)
        {
            await _employeeDetilsRepository.UpdateEmployeeAsync(employeeid,employee);
            return Ok(employee);
        }

        [HttpDelete("{employeeid}")]
        public async Task<IActionResult> DeleteEmployeeDetails([FromRoute] int employeeid)
        {
            await _employeeDetilsRepository.DeleteEmployeeAsync(employeeid);
            return Ok();
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> EmpPicCreate([FromRoute] int id,[FromForm] IFormFile file)
        {
            string str=await _employeeDetilsRepository.AddempPicAsync(file);
            return Ok(str);

        }

    }
}
