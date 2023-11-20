using DataLib;
using Microsoft.Extensions.Options;
using ResLib;
using System.Text.Json.Serialization;
using ApiLib;

namespace StudentManagement
{
	public class Startup
	{
		private IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// Configuration related to your API
			services.AddControllers()
				.AddJsonOptions(x =>
				{
					x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				});

			// Add your other services
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddHttpContextAccessor();
			services.AddScoped<LocalizationHelper>();

			// Configure Swagger
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.ConfigureDataServices(connectionString);
			services.ConfigureApiServices();
			services.ConfigureResServices();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseRouting();
			app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
			app.UseHttpsRedirection();

			// Global CORS policy
			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
