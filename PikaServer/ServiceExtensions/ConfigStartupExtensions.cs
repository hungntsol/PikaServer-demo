using PikaServer.Infras.AppSettings;
using PikaServer.Infras.Constants;
using PikaServer.Infras.Services;

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
		RegisterAppSettings(services, configuration);
		RegisterHttpClients(services, configuration);
		RegisterServices(services, configuration);

		return services;
	}

	private static IServiceCollection RegisterAppSettings(IServiceCollection services, IConfiguration configuration)
	{
		services.Configure<HDBankApiSetting>(configuration.GetSection("HDBankApi"));

		return services;
	}

	private static IServiceCollection RegisterHttpClients(IServiceCollection services, IConfiguration configuration)
	{
		var authEndpoint = configuration.GetValue<string>("HDBankApi:AuthUrl");
		var apiEndpoint = configuration.GetValue<string>("HDBankApi:BaseUrl");

		services.AddHttpClient(HttpClientConstants.HDBankAuthClientName,
			http =>
			{
				http.BaseAddress = new Uri(authEndpoint);
				http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
			});

		services.AddHttpClient(HttpClientConstants.HDBankClientName, http =>
		{
			var apiKey = configuration.GetValue<string>("HDBankApi:ApiKey");
			http.BaseAddress = new Uri(apiEndpoint);
			http.DefaultRequestHeaders.Add("x-api-key", apiKey);
		});

		return services;
	}

	private static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IHDBankAuthService, HDBankAuthService>();
		services.AddTransient<IHDBankTokenManager, HDBankTokenManager>();

		return services;
	}
}