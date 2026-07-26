# Hands-On 3 — Web API using custom model class

Covers:
- Custom model class (`Employee`, with nested `Department`/`Skill`), returning
  a list of a custom entity from an action method
- `[FromBody]` for reading a model object from the request body
- Custom action filter (`CustomAuthFilter`) via `ActionFilterAttribute` /
  `OnActionExecuting`
- Custom exception filter (`CustomExceptionFilter`) via `IExceptionFilter`

## What's here

- `Models/Employee.cs`, `Models/Department.cs`, `Models/Skill.cs` — the exact
  structure specified in the exercise.
- `Controllers/EmployeeController.cs` — constructor seeds a hardcoded list via
  `GetStandardEmployeeList()`; `GetStandrad()` returns `List<Employee>` with
  `ProducesResponseType` 200; `Create` demonstrates `[FromBody]`; `ThrowError`
  deliberately throws to exercise the exception filter.
- `Filters/CustomAuthFilter.cs` — checks for an `Authorization` header and that
  it contains `Bearer`; applied at the controller level.
- `Filters/CustomExceptionFilter.cs` — logs exception details to
  `exception-log.txt` and returns a 500.

> Note: the original doc's `WebApiCompatShim` NuGet package is obsolete on
> .NET 8 — `IExceptionFilter` is built into `Microsoft.AspNetCore.Mvc.Filters`
> already, so it isn't referenced in the `.csproj`.

## Try it

```bash
dotnet restore
dotnet run
```

1. `GET /api/Emp` in Swagger — check the response schema/status for 200.
2. Call any `Emp` endpoint via Postman **without** an `Authorization` header →
   `400 Bad Request`, `"Invalid request - No Auth token"`.
3. Add an `Authorization` header that doesn't say "Bearer" → `400`,
   `"Invalid request - Token present but Bearer unavailable"`.
4. Add `Authorization: Bearer anything` → request goes through.
5. `GET /api/Emp/error` → 500, and check `exception-log.txt` (created next to
   the project) for the logged stack trace.

> Written without nuget.org access in this sandbox — `dotnet restore && dotnet build` yourself before pushing.
