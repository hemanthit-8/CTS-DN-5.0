using Microsoft.AspNetCore.Mvc;
using EmployeeWebApi.Models;
using EmployeeWebApi.Filters;

namespace EmployeeWebApi.Controllers
{
    /// <summary>
    /// Hands-On 3: rebuilt around the custom 'Employee' model class.
    ///  - Task 1: GetStandardEmployeeList, GET returning List&lt;Employee&gt;
    ///    with ProducesResponseType 200.
    ///  - Task 2: CustomAuthFilter applied at the controller level to check
    ///    for an Authorization / Bearer header.
    ///  - Task 3: a dedicated endpoint that throws, to exercise
    ///    CustomExceptionFilter (registered via [ServiceFilter]).
    /// </summary>
    [ApiController]
    [Route("api/Emp")]
    [CustomAuthFilter]
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _employees = new();

        public EmployeeController()
        {
            if (_employees.Count == 0)
            {
                _employees = GetStandardEmployeeList();
            }
        }

        private List<Employee> GetStandardEmployeeList()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Asha Rao",
                    Salary = 65000,
                    Permanent = true,
                    Department = new Department { Id = 1, Name = "Engineering" },
                    Skills = new List<Skill> { new Skill { Id = 1, Name = "C#" }, new Skill { Id = 2, Name = "SQL" } },
                    DateOfBirth = new DateTime(1994, 3, 12),
                },
                new Employee
                {
                    Id = 2,
                    Name = "Rahul Mehta",
                    Salary = 72000,
                    Permanent = true,
                    Department = new Department { Id = 2, Name = "QA" },
                    Skills = new List<Skill> { new Skill { Id = 3, Name = "Selenium" } },
                    DateOfBirth = new DateTime(1991, 7, 24),
                },
                new Employee
                {
                    Id = 3,
                    Name = "Priya Nair",
                    Salary = 58000,
                    Permanent = false,
                    Department = new Department { Id = 1, Name = "Engineering" },
                    Skills = new List<Skill> { new Skill { Id = 1, Name = "C#" }, new Skill { Id = 4, Name = "Angular" } },
                    DateOfBirth = new DateTime(1997, 11, 2),
                },
            };
        }

        // GET: api/Emp
        // Modify the return type to List<Employee> and add ProducesResponseType 200.
        [HttpGet]
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        public ActionResult<List<Employee>> GetStandrad()
        {
            return Ok(_employees);
        }

        // GET: api/Emp/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> GetById(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return BadRequest("Invalid employee id");
            }
            return Ok(employee);
        }

        // POST: api/Emp
        [HttpPost]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
        public ActionResult<Employee> Create([FromBody] Employee employee)
        {
            employee.Id = _employees.Count == 0 ? 1 : _employees.Max(e => e.Id) + 1;
            _employees.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // GET: api/Emp/error
        // Task 3: deliberately throws, to exercise CustomExceptionFilter.
        [HttpGet("error")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Employee> ThrowError()
        {
            throw new InvalidOperationException("Simulated failure to test CustomExceptionFilter.");
        }
    }
}
