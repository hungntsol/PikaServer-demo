using Microsoft.Extensions.DependencyInjection;
using PikaServer.Infras.AppSettings;
using PikaServer.Infras.Constants;
using PikaServer.Infras.Services;

namespace PikaServer.Infras;

public static class DependencyInjections
{
	public static IServiceCollection UseHDBankAuth(this IServiceCollection services, Action<HDBankApiSetting> options)
	{
		// register setting options
		services.Configure(options);

		var hdBankApiSetting = new HDBankApiSetting();
		options.Invoke(hdBankApiSetting);

		// register httpClient
		services.AddHttpClient(HttpClientConstants.HDBankAuthClientName,
			http =>
			{
				http.BaseAddress = new Uri(hdBankApiSetting.AuthUrl);
				http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
			});

		services.AddHttpClient(HttpClientConstants.HDBankClientName, http =>
		{
			http.BaseAddress = new Uri(hdBankApiSetting.BaseUrl);
			http.DefaultRequestHeaders.Add("x-api-key", hdBankApiSetting.ClientId);
		});

		// register transient services
		services.AddTransient<IHDBankAuthService, HDBankAuthService>();

		return services;
	}
}
