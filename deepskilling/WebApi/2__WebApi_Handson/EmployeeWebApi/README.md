# Hands-On 2 — Web API using .NET Core with Swagger

Covers:
- Swagger installation (`Swashbuckle.AspNetCore`), `AddSwaggerGen`/`UseSwaggerUI`,
  `ProducesResponseType`
- Testing with Postman (headers, body, request collections)
- Route/ActionName attributes; renaming a controller's route

## What's here

- `Program.cs` — Swagger registered with a title, version, and contact/license
  details, exactly as the exercise's code snippet specifies.
- `Controllers/EmployeeController.cs` — a basic Employee controller whose route
  was changed from the default `api/Employee` to **`api/Emp`** (step 3 of Task 1).

## Try it

```bash
dotnet restore
dotnet run
```

1. Open `/swagger` — confirm "Swagger Demo" title/version/contact show at the
   top, and both `Values` and `Emp` controllers are listed.
2. Click a `GET`, "Try it out", "Execute".
3. In Postman: `GET https://localhost:<port>/api/Emp` — confirm the employee
   list appears in the response Body with status `200`.
4. Confirm `GET .../api/Employee` (the old default route) now returns 404,
   while `/api/Emp` works — this is the route-rename check from step 3.

> Written without nuget.org access in this sandbox — `dotnet restore && dotnet build` yourself before pushing.
