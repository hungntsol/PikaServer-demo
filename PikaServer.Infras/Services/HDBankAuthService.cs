using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PikaServer.Infras.AppSettings;
using PikaServer.Infras.Constants;
using PikaServer.Infras.HDBankHttpSchemas;

namespace PikaServer.Infras.Services;

public interface IHDBankAuthService
{
	Task<OAuth2Response> OAuth2Async(CancellationToken cancellationToken = default);
}

public class HDBankAuthService : IHDBankAuthService
{
	private const string OAuth2GrantType = "refresh_token";

	private readonly HDBankApiSetting _hdBankApiSetting;
	private readonly IHDBankTokenManager _hdBankTokenManager;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILogger<HDBankAuthService> _logger;

	public HDBankAuthService(IOptions<HDBankApiSetting> hdBankApiSettingOption,
		ILogger<HDBankAuthService> logger,
		IHDBankTokenManager hdBankTokenManager, IHttpClientFactory httpClientFactory)
	{
		_logger = logger;
		_hdBankTokenManager = hdBankTokenManager;
		_httpClientFactory = httpClientFactory;
		_hdBankApiSetting = hdBankApiSettingOption.Value;
	}

	public async Task<OAuth2Response> OAuth2Async(CancellationToken cancellationToken = default)
	{
		var httpClient = _httpClientFactory.CreateClient(HttpClientConstants.HDBankClientName);
		var body = new OAuth2TokenRequest(_hdBankApiSetting.ClientId, _hdBankApiSetting.RefreshToken, OAuth2GrantType);

		var response = await httpClient.PostAsJsonAsync("/oauth2/token", body, cancellationToken: cancellationToken);
		if (response.StatusCode != HttpStatusCode.OK)
		{
			_logger.LogError("Perform OAuth2 fail with error {statusCode}, {error}",
				response.StatusCode, response.Content.ReadAsStringAsync(cancellationToken));
		}

		var responseData =
			await response.Content.ReadFromJsonAsync<OAuth2Response>(cancellationToken: cancellationToken);
		if (responseData is not null)
		{
			return responseData;
		}

		_logger.LogError("Cannot read HDBank OAuth2 response data due to null");
		throw new Exception("Service Unavailable");
	}
}