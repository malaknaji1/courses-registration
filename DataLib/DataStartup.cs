using DataLib.Data;
using DataLib.Data.Repositories.Impl;
using DataLib.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataLib
{
	public static class DataStartup
	{
		public static IServiceCollection ConfigureDataServices(this IServiceCollection services, string connectionString) 
		{
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlite(connectionString);
			});

			services.AddScoped<IStudentRepository, StudentRepository>();
			services.AddScoped<ICourseRepository, CourseRepository>();
			services.AddScoped<IStudentCoursesRepository, StudentCoursesRepository>();
			services.AddScoped<ILookupsRepository, LookupsRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
			services.AddScoped<PermissionAdder>();
			return services;
		}
	}
}