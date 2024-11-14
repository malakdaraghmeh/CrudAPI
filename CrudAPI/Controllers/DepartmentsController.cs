using CrudAPI.Data;
using CrudAPI.DTOs.Department;
using CrudAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var department = context.Departments.Select(
                x => new GetDepartmentsDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                }
                );
            return Ok(department);
        }

        [HttpGet("Details")]
        public IActionResult GetById(int Id)
        {
            var department = context.Departments.Find(Id);
            if (department is null)
            {
                return NotFound("department not found");
            }
            var depDto = new DetailsDepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
            };

            return Ok(depDto);
        }
        [HttpPost("Create")]
        public IActionResult Create(CreateDepartmentDto depDto)
        {
            Department dep = new Department()
            {
                Name = depDto.Name
            };
            context.Departments.Add(dep);
            context.SaveChanges();
            return Ok(dep);

        }

        [HttpPut("Update")]
        public IActionResult Update(int Id, UpdateDepartmentsDto departmentDto)
        {
            var current = context.Departments.Find(Id);
            if (current is null)
            {
                return NotFound("department not found");
            }
            current.Name = departmentDto.Name;
            context.SaveChanges();
            var depDto = new UpdateDepartmentsDto
            {
                Name = departmentDto.Name,
            };
            return Ok(depDto);
        }

        [HttpDelete("Remove")]
        public IActionResult Remove(int Id)
        {
            var department = context.Departments.Find(Id);
            if (department is null)
            {
                return NotFound("department not found");
            }
            var departmentDto = new RemoveDepartmentsDto
            {
                Name = department.Name 
            };

            context.Departments.Remove(department);
            context.SaveChanges();
            return Ok(departmentDto);
        }
    }
}
