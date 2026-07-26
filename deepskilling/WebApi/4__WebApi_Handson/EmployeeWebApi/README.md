# Hands-On 4 — Web API CRUD operation

Covers:
- `[FromBody]` to extract data into the custom model class
- Updating hardcoded data thru a `PUT` action method
- Testing with Swagger and Postman

## What's here

`Controllers/EmployeeController.cs` — added `Update(int id, [FromBody] Employee updatedEmployee)`:

- `id <= 0` → `400 Bad Request`, `"Invalid employee id"`.
- `id > 0` but not present in the hardcoded employee list → same `400` message.
- Otherwise → updates the in-memory record from the request body and returns it.

## Try it

```bash
dotnet restore
dotnet run
```

In Swagger or Postman, `PUT /api/Emp/{id}` with a JSON `Employee` body:

- `id = 0` or negative → 400.
- `id = 999` (not in the seed list) → 400.
- `id = 1` with a modified `Name`/`Salary` → 200, returns the updated employee.

> Written without nuget.org access in this sandbox — `dotnet restore && dotnet build` yourself before pushing.
