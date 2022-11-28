using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PikaServer.Common.Utils;
using PikaServer.Infras.AppSettings;
using PikaServer.Infras.Constants;
using PikaServer.Infras.HDBankHttpSchemas;
using PikaServer.Infras.Services.Credential;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Infras.Services.Auth;

public class HDBankAuthService : IHDBankAuthService
{
	private const string OAuth2GrantType = "refresh_token";

	private readonly HDBankApiSetting _hdBankApiSetting;
	private readonly HDBankCredential _hdBankCredential;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILogger<HDBankAuthService> _logger;

	public HDBankAuthService(IOptions<HDBankApiSetting> hdBankApiSettingOption,
		ILogger<HDBankAuthService> logger,
		IHttpClientFactory httpClientFactory,
		HDBankCredential hdBankCredential)
	{
		_logger = logger;
		_httpClientFactory = httpClientFactory;
		_hdBankCredential = hdBankCredential;
		_hdBankApiSetting = hdBankApiSettingOption.Value;
	}

	public async Task<OAuth2Response> OAuth2Async(CancellationToken cancellationToken = default)
	{
		var httpClient = _httpClientFactory.CreateClient(HttpClientConstants.HDBankAuthClientName);
		var formContent = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("client_id", _hdBankApiSetting.ClientId),
			new KeyValuePair<string, string>("grant_type", "refresh_token"),
			new KeyValuePair<string, string>("refresh_token", _hdBankApiSetting.RefreshToken)
		});

		var httpResponse = await httpClient.PostAsync("/oauth2/token", formContent, cancellationToken);
		if (httpResponse.StatusCode != HttpStatusCode.OK)
		{
			_logger.LogError("Perform OAuth2 fail with error {statusCode}, {error}",
				httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync(cancellationToken));
		}

		var responseData =
			await httpResponse.Content.ReadFromJsonAsync<OAuth2Response>(cancellationToken: cancellationToken);
		if (responseData is not null)
		{
			_logger.LogInformation("Perform OAuth2 success: {data}", PikaJsonConvert.SerializeObject(responseData));

			_hdBankCredential.SetAccessToken(responseData.AccessToken);
			_hdBankCredential.SetIdToken(responseData.IdToken);

			return responseData;
		}

		_logger.LogError("Cannot read HDBank OAuth2 response data due to null");
		throw new Exception("Service Unavailable");
	}
}
