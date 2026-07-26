using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeWebApi.Models;
using EmployeeWebApi.Filters;

namespace EmployeeWebApi.Controllers
{
    /// <summary>
    /// Built up across Hands-On 2, 3 and 4:
    ///  - HO2: Swagger + Postman testing target; route renamed from the default
    ///    "api/Employee" to "api/Emp" (HO2, Task 1, step 3).
    ///  - HO3: custom Employee model, GetStandardEmployeeList, CustomAuthFilter
    ///    (later superseded — see note below), CustomExceptionFilter demo.
    ///  - HO4: PUT-based update with validation.
    ///
    /// HO5 note: CustomAuthFilter was applied here in HO3 to manually check for
    /// a Bearer token. Hands-On 5, Task 2 explicitly says to remove it in favour
    /// of real JWT validation, so this controller now uses [Authorize] instead
    /// (see the class-level attribute below) once AuthController exists.
    /// </summary>
    [ApiController]
    [Route("api/Emp")]
    [Authorize(Roles = "POC,Admin")] // Hands-On 5, Task 4: roles allowed to access this controller.
    [ServiceFilter(typeof(CustomExceptionFilter))]
    public class EmployeeController : ControllerBase
    {
        // Hands-On 3: hardcoded in-memory data created in the constructor.
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
        // Hands-On 3, step: return List<Employee> with ProducesResponseType 200.
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
        public ActionResult<List<Employee>> GetStandrad()
        {
            return Ok(_employees);
        }

        // GET: api/Emp/5
        [HttpGet("{id}")]
        [AllowAnonymous]
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

        // GET: api/Emp/error
        // Hands-On 3, Task 3: deliberately throws, to exercise CustomExceptionFilter.
        [HttpGet("error")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Employee> ThrowError()
        {
            throw new InvalidOperationException("Simulated failure to test CustomExceptionFilter.");
        }

        // POST: api/Emp
        // Hands-On 3/4: create a new employee from the request body.
        [HttpPost]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
        public ActionResult<Employee> Create([FromBody] Employee employee)
        {
            employee.Id = _employees.Count == 0 ? 1 : _employees.Max(e => e.Id) + 1;
            _employees.Add(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        // PUT: api/Emp/5
        // Hands-On 4: update employee data as per the input, with the exact
        // validation rules the exercise specifies.
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> Update(int id, [FromBody] Employee updatedEmployee)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var existing = _employees.FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                return BadRequest("Invalid employee id");
            }

            existing.Name = updatedEmployee.Name;
            existing.Salary = updatedEmployee.Salary;
            existing.Permanent = updatedEmployee.Permanent;
            existing.Department = updatedEmployee.Department;
            existing.Skills = updatedEmployee.Skills;
            existing.DateOfBirth = updatedEmployee.DateOfBirth;

            var result = _employees.Where(e => e.Id == id);
            return Ok(result);
        }

        // DELETE: api/Emp/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                return BadRequest("Invalid employee id");
            }

            _employees.Remove(existing);
            return Ok($"Deleted employee {id}");
        }
    }
}
