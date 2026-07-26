using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApi.Filters
{
    /// <summary>
    /// Hands-On 3, Task 2: custom action filter for a lightweight, manual
    /// "does this look like a Bearer token" check — intercepts the request
    /// BEFORE the action method runs via OnActionExecuting.
    ///
    /// NOTE: from Hands-On 5 onward this is superseded by real JWT validation
    /// via [Authorize]/AddJwtBearer — see AuthController and Program.cs. This
    /// filter is kept here to satisfy Hands-On 3 exactly as written; it is not
    /// applied to EmployeeController anymore once the [Authorize] attribute
    /// takes over (Hands-On 5, Task 2 explicitly says to remove it).
    /// </summary>
    public class CustomAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Result = new BadRequestObjectResult("Invalid request - No Auth token");
                return;
            }

            if (!authHeader.ToString().Contains("Bearer"))
            {
                context.Result = new BadRequestObjectResult("Invalid request - Token present but Bearer unavailable");
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
