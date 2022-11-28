using Microsoft.Extensions.DependencyInjection;
using PikaServer.Infras.AppSettings;

namespace PikaServer.ServiceExtensions;

public static class ConfigStartupServiceExtensions
{
	/// <summary>
	/// Config service for application startup
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	public static IServiceCollection ConfigStartup(this IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<HDBankApiAppSetting>(configuration.GetSection("HDBankApi"));

		return services;
	}
}