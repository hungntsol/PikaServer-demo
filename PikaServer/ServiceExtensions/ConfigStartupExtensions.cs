using PikaServer.Infras;

namespace PikaServer.Api.ServiceExtensions;

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
		RegisterAppSettings(services, configuration);
		RegisterServices(services, configuration);

		return services;
	}

	private static IServiceCollection RegisterAppSettings(IServiceCollection services, IConfiguration configuration)
	{
		return services;
	}

	private static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
	{
		services.UseHdBankVendor(opt => configuration.GetSection("HDBankApi").Bind(opt));

		return services;
	}
}
