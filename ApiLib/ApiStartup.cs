using DataLib;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ResLib;
using System.Text.Json.Serialization;
using DataLib.Data.Repositories.Impl;
using DataLib.Data.Repositories.Interface;
using Microsoft.Extensions.Hosting;

namespace ApiLib
{
	public static class StartupExtension
	{
		public static IServiceCollection ConfigureApiServices(
			this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
			services.AddScoped<IStudentRepository, StudentRepository>();
			services.AddScoped<ICourseRepository, CourseRepository>();
			services.AddScoped<IStudentCoursesRepository, StudentCoursesRepository>();
			services.AddScoped<ILookupsRepository, LookupsRepository>();
			return services;
		}
	}
}