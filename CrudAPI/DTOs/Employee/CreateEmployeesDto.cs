namespace CrudAPI.DTOs.Employee
{
    public class CreateEmployeesDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId {  get; set; }
    }
}
