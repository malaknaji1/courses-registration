using courses_registration;
using courses_registration.Controllers;
using courses_registration.Data;
using courses_registration.DTO;
using courses_registration.Helpers;
using courses_registration.Interfaces;
using courses_registration.Repositories;
using courses_registration.Seeders;
using courses_registration.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;

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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILookupRepository, LookupRepository>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IValidator<CourseDTO>, CourseValidator>();
builder.Services.AddTransient<IValidator<StudentDTO>, StudentValidator>();
builder.Services.AddTransient<IValidator<PrerequisiteDTO>, PrerequisiteValidator>();
builder.Services.AddTransient<IValidator<EnrollmentDTO>, EnrollmentValidator>();
builder.Services.AddTransient<IValidator<UserDTO>, UserValidator>();
builder.Services.AddLocalization();
var localizationOptions = new RequestLocalizationOptions();
var supportedCultures = new[]
{
    new CultureInfo("en_US"),
    new CultureInfo("ar"),

};


localizationOptions.SupportedCultures = supportedCultures;
localizationOptions.SupportedUICultures = supportedCultures;
localizationOptions.SetDefaultCulture("en-US");
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
builder.Services.AddScoped<Localizer>();
builder.Services.AddScoped<BasicAuthFilter>();


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
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
