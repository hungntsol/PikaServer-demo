using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using PikaServer.Infras.AppSettings;
using PikaServer.Infras.Constants;
using PikaServer.Infras.Services.Auth;
using PikaServer.Infras.Services.Credential;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Infras;

public static class DependencyInjection
{
	public static IServiceCollection UseHDBankVendor(this IServiceCollection services, Action<HDBankApiSetting> options)
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
			http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			http.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", hdBankApiSetting.ApiKey);
		});

		// register transient services
		services.AddTransient<IHDBankAuthService, HDBankAuthService>();
		services.AddTransient<IHDBankCredentialManager, HDBankCredentialManager>();

		// singleton
		services.AddSingleton<HDBankCredential>();

		return services;
	}
}
