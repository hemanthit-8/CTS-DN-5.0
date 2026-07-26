using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeWebApi.Filters
{
    /// <summary>
    /// Hands-On 3, Task 3: global exception filter. Catches unhandled
    /// exceptions raised inside action methods, logs the detail to a file,
    /// and returns a 500 Internal Server Error result.
    ///
    /// NOTE: the original exercise references the WebApiCompatShim NuGet
    /// package, which only existed for migrating ASP.NET Web API 2 code onto
    /// early ASP.NET Core (1.x/2.x). IExceptionFilter has been a first-class,
    /// built-in part of Microsoft.AspNetCore.Mvc.Filters ever since — no extra
    /// package is needed on .NET 8, so it's intentionally left out of the .csproj.
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public CustomExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var logPath = Path.Combine(_env.ContentRootPath, "exception-log.txt");
            var logEntry = $"{DateTime.UtcNow:O} - {context.Exception.GetType().Name}: {context.Exception.Message}{Environment.NewLine}{context.Exception.StackTrace}{Environment.NewLine}{new string('-', 40)}{Environment.NewLine}";

            File.AppendAllText(logPath, logEntry);

            context.Result = new ObjectResult(new
            {
                error = "An unexpected error occurred.",
                detail = context.Exception.Message,
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            context.ExceptionHandled = true;
        }
    }
}
