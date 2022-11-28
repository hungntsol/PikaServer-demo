using PikaServer.Infras.AppSettings;

namespace PikaServer.ServiceExtensions;

public static class ConfigStartupExtensions
{
	/// <summary>
	/// Config service for application startup
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	public static IServiceCollection ConfigStartup(this IServiceCollection services, IConfiguration configuration)
	{
		// Read settings env
		services.Configure<HDBankApiSetting>(configuration.GetSection("HDBankApi"));

		return services;
	}
}