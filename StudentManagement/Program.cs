using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// after AddDbContext(...)
builder.Services.AddScoped(typeof(StudentManagement.Repositories.IRepository<>), typeof(StudentManagement.Repositories.Repository<>));

builder.Services.AddScoped<StudentIdGenerator>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();