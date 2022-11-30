﻿using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PikaServer.Infras.AppSettings;
using PikaServer.Infras.Constants;
using PikaServer.Infras.DelegateHandler;
using PikaServer.Infras.Helpers;
using PikaServer.Infras.Services.ApiFeature;
using PikaServer.Infras.Services.Auth;
using PikaServer.Infras.Services.Credential;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Infras;

public static class DependencyInjection
{
	public static IServiceCollection UseHdBankVendor(this IServiceCollection services, Action<HdBankApiSetting> options)
	{
		// register config options
		services.Configure(options);

		var hdBankApiSetting = new HdBankApiSetting();
		options.Invoke(hdBankApiSetting);


		// singleton
		services.AddSingleton<HdBankCredential>();
		services.AddScoped<HdBankHttpHandler>();

		// register transient services
		services.AddTransient<IHdBankAuthService, HdBankAuthService>();
		services.AddTransient<IHdBankCredentialManager, HdBankCredentialManager>();
		services.AddTransient<IHdBankBasicFeature, HdBankBasicFeature>();
		services.AddTransient<RsaCredentialHelper>();

		// register httpClient
		services.AddHttpClient(HttpClientNameConstants.HdBankAuth,
			http =>
			{
				http.BaseAddress = new Uri(hdBankApiSetting.AuthUrl);
				http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
			});

		services.AddHttpClient(HttpClientNameConstants.HdBank, http =>
			{
				http.BaseAddress = new Uri(hdBankApiSetting.BaseUrl);
				http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
				http.DefaultRequestHeaders.TryAddWithoutValidation("x-api-key", hdBankApiSetting.ApiKey);
			})
			.AddHttpMessageHandler(s => s.GetRequiredService<HdBankHttpHandler>());

		return services;
	}

	public static IServiceCollection UseJwtAuthentication(this IServiceCollection services,
		Action<JwtAuthSetting> config)
	{
		services.Configure(config);
		var jwtSetting = new JwtAuthSetting();
		config.Invoke(jwtSetting);

		services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret)),
					ValidateAudience = false,
					ValidateIssuer = false
				};
			});

		services.AddAuthorization();

		services.AddTransient<IJwtAuthService, JwtAuthService>();

		return services;
	}
}
