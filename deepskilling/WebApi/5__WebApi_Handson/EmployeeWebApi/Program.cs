using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using EmployeeWebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------------------------
// Hands-On 1: services registration. In classic ASP.NET Core (2.x/3.x) this
// lived in Startup.ConfigureServices; the minimal-hosting model (net6.0+)
// folds Startup.cs's two methods into this single Program.cs, but the
// concepts (DI container, service registration, middleware pipeline) are
// exactly the same ones described in the objectives for Hands-On 1.
// ---------------------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Hands-On 3, Task 3: register the exception filter for DI (ServiceFilter).
builder.Services.AddScoped<CustomExceptionFilter>();

// ---------------------------------------------------------------------------
// Hands-On 2: Swagger / Swashbuckle setup.
// (Modern Swashbuckle uses Microsoft.OpenApi's OpenApiInfo/OpenApiContact/
// OpenApiLicense — the same fields the exercise's Info/Contact/License refer to.)
// ---------------------------------------------------------------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger Demo",
        Version = "v1",
        Description = "TBD",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact { Name = "John Doe", Email = "john@xyzmail.com", Url = new Uri("https://www.example.com") },
        License = new OpenApiLicense { Name = "License Terms", Url = new Uri("https://www.example.com") },
    });

    // Lets Swagger UI's "Authorize" button attach a Bearer token to requests
    // (needed to try out EmployeeController's endpoints once [Authorize] is applied).
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
            },
            Array.Empty<string>()
        },
    });
});

// ---------------------------------------------------------------------------
// Hands-On 5, Task: CORS — allows a local front-end app (e.g. the Angular
// Student Course Portal on http://localhost:4200) to call this API.
// ---------------------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ---------------------------------------------------------------------------
// Hands-On 5: JWT Bearer authentication setup — issuer, audience and signing
// key must match what AuthController.GenerateJSONWebToken uses.
// ---------------------------------------------------------------------------
var jwtKey = builder.Configuration["Jwt:Key"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = symmetricSecurityKey,
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalApp");

// Order matters: authentication before authorization.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
