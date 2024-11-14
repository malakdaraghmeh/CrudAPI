using CrudAPI.Data;
using CrudAPI.DTOs.Employee;
using CrudAPI.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var employee = context.Employees.ToList();
            var response = employee.Adapt<IEnumerable<GetEmployeeDto>>();
            return Ok(response);
        }

        [HttpGet("Details")]
        public IActionResult GetById(int Id)
        {
            var employee = context.Employees.Find(Id);
            if (employee is null)
            {
                return NotFound("employee not found");
            }
            var response=employee.Adapt<DetailsEmployeesDto>();

            return Ok(response);
        }
        [HttpPost("Create")]
        public IActionResult Create(CreateEmployeesDto empDto)
        {
            var employee=empDto.Adapt<Employee>();
            context.Employees.Add(employee);
            context.SaveChanges();
            return Ok();
           
        }

        [HttpPut("Update")]
        public IActionResult Update(int Id, UpdateEmployeesDto empDto)
        {
            var current = context.Employees.Find(Id);
            if (current is null)
            {
                return NotFound("employee not found");
            }
            empDto.Adapt(current);
            context.SaveChanges();
            var emp = current.Adapt<UpdateEmployeesDto>();
            return Ok(emp);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(int Id)
        {
            var employee = context.Employees.Find(Id);
            if (employee is null)
            {
                return NotFound("employee not found");
            }
            context.Employees.Remove(employee);
            var Emp = employee.Adapt<RemoveEmployeesDto>();
            context.SaveChanges();
            return Ok(Emp);
        }

    }
}



