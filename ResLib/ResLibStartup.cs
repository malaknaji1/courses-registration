using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;


namespace ResLib
{
	public static class ResLibStartup
	{
		public static IServiceCollection ConfigureResServices(this IServiceCollection services)
		{
			services.Configure<RequestLocalizationOptions>(options =>
			{
				var cultures = new[]
				{
					new CultureInfo("en"),
					new CultureInfo("ar")
				};
				options.DefaultRequestCulture = new RequestCulture("en");
				options.SupportedCultures = cultures;
				options.SupportedUICultures = cultures;
			});
			services.AddTransient<LocalizationHelper>();
			return services;
		} 
	}
} 