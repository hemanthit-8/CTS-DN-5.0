using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    /// <summary>
    /// Hands-On 2: a basic Employee controller (a simple record shape, before
    /// the full custom 'Employee' model class arrives in Hands-On 3), used to
    /// demonstrate Swagger + Postman testing, and the controller-route rename
    /// from the default "api/Employee" to "api/Emp" (Task 1, step 3).
    /// </summary>
    [ApiController]
    [Route("api/Emp")]
    public class EmployeeController : ControllerBase
    {
        public record EmployeeSummary(int Id, string Name, string Role);

        private static readonly List<EmployeeSummary> _employees = new()
        {
            new EmployeeSummary(1, "Asha Rao", "Engineer"),
            new EmployeeSummary(2, "Rahul Mehta", "QA"),
            new EmployeeSummary(3, "Priya Nair", "Engineer"),
        };

        // GET: api/Emp
        [HttpGet]
        public ActionResult<List<EmployeeSummary>> Get()
        {
            return Ok(_employees);
        }

        // GET: api/Emp/5
        [HttpGet("{id}")]
        public ActionResult<EmployeeSummary> Get(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            return employee == null ? NotFound() : Ok(employee);
        }
    }
}
