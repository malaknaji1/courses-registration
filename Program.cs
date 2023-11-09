using courses_registration.Data;
using courses_registration.DTO;
using courses_registration.Interfaces;
using courses_registration.Repositories;
using courses_registration.Seeders;
using courses_registration.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("SqliteConnection")
));
builder.Services.AddTransient<DatabaseSeeder>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IPrerequisiteRepository, PrerequisiteRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IValidator<CourseDTO>, CourseValidator>();
builder.Services.AddTransient<IValidator<StudentDTO>, StudentValidator>();
builder.Services.AddTransient<IValidator<PrerequisiteDTO>, PrerequisiteValidator>();
builder.Services.AddTransient<IValidator<EnrollmentDTO>, EnrollmentValidator>();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DatabaseSeeder>();
        service.SeedDataContext();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
