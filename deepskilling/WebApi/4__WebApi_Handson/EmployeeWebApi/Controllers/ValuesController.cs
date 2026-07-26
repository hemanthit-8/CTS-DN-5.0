using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Controllers
{
    /// <summary>
    /// Hands-On 1: the classic "first Web API" controller — mirrors the
    /// ValuesController that Visual Studio's older Web API scaffolding used to
    /// generate with Read/Write action methods for every HTTP verb.
    /// ApiController + ControllerBase gives you automatic model-state
    /// validation, binding source inference, and problem-details responses
    /// (the modern equivalent of inheriting from ApiController in .NET Framework).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(new[] { "value1", "value2" });
        }

        // GET: api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok($"value-{id}");
        }

        // POST: api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return StatusCode(StatusCodes.Status201Created, value);
        }

        // PUT: api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok($"Updated {id} with {value}");
        }

        // DELETE: api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Deleted {id}");
        }
    }
}
