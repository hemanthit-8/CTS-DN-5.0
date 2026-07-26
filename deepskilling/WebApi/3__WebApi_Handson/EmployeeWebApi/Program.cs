using Microsoft.OpenApi.Models;
using EmployeeWebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Hands-On 3, Task 3: register the exception filter for DI (ServiceFilter).
builder.Services.AddScoped<CustomExceptionFilter>();

// Hands-On 2: Swagger setup with title/version/contact/license — modern
// Swashbuckle uses Microsoft.OpenApi's OpenApiInfo/OpenApiContact/OpenApiLicense
// for what the original doc calls Info/Contact/License.
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
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
