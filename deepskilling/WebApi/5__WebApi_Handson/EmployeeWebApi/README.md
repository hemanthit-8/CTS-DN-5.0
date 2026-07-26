# Hands-On 5 — CORS, JWT Authentication & Roles

Covers:
- What is CORS, enabling it via `Startup`/`Program.cs`, `AddCors`/`UseCors`
- JWT ("Json Web Token") authentication: `AddAuthentication` + `AddJwtBearer`,
  `TokenValidationParameters`, `UseAuthentication`
- `AuthController` with `[AllowAnonymous]` to generate tokens, and `Claims`
- `[Authorize]` with roles on `EmployeeController`

## What's here

- `Program.cs` — `AddCors("AllowLocalApp")` for `http://localhost:4200`,
  JWT bearer authentication wired to `Jwt:Key`/`Jwt:Issuer`/`Jwt:Audience`
  in `appsettings.json` (must match `AuthController`'s token-generation values).
- `Controllers/AuthController.cs` — `[AllowAnonymous]`; `GET /api/Auth/token`
  generates a JWT with a `Role` claim, expiring in 2 minutes (Task 3).
- `Controllers/EmployeeController.cs` — `CustomAuthFilter` removed (per Task 2);
  replaced with `[Authorize(Roles = "POC,Admin")]` (Task 4).

## Try it

```bash
dotnet restore
dotnet run
```

1. `GET /api/Emp` without an `Authorization` header → `401 Unauthorized`.
2. `GET /api/Auth/token?userId=1&userRole=Admin` → copy the returned token.
3. Add `Authorization: Bearer <token>` header, retry `GET /api/Emp` → `200 OK`.
4. Tamper with a character in the token → `401` again.
5. Wait 2+ minutes after generating a token, then retry → `401` (expired).
6. Roles: temporarily change the controller attribute to
   `[Authorize(Roles = "POC")]` and hit `GET /api/Emp` with an `Admin`-role
   token → `401` (no matching role). Change it back to include `"Admin"` →
   `200 OK` again.

> Written without nuget.org access in this sandbox — `dotnet restore && dotnet build` yourself before pushing.
