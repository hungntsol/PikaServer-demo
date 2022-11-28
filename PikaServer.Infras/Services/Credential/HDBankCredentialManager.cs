using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using PikaServer.Infras.Constants;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Infras.Services.Credential;

public class HDBankCredentialManager : IHDBankCredentialManager
{
	private readonly IHDBankAuthService _authService;
	private readonly HDBankCredential _credential;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILogger<HDBankCredentialManager> _logger;

	public HDBankCredentialManager(HDBankCredential credential, IHDBankAuthService authService,
		IHttpClientFactory httpClientFactory, ILogger<HDBankCredentialManager> logger)
	{
		_credential = credential;
		_authService = authService;
		_httpClientFactory = httpClientFactory;
		_logger = logger;
	}


	public async Task<string> GetPublicKeyAsync(CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrEmpty(_credential.RsaPublicKey))
		{
			await ClaimPublicKeyAsync(cancellationToken);
		}

		return _credential.RsaPublicKey!;
	}

	public string GetPublicKey()
	{
		return GetPublicKeyAsync().Result;
	}

	public async Task ClaimPublicKeyAsync(CancellationToken cancellationToken = default)
	{
		await _authService.OAuth2Async(cancellationToken);

		var httpClient = _httpClientFactory.CreateClient(HttpClientConstants.HDBankClientName);
		httpClient.DefaultRequestHeaders.TryAddWithoutValidation("access-token", _credential.IdToken);

		var httpResponse = await httpClient.GetAsync("dev/get_key", cancellationToken: cancellationToken);
		if (!httpResponse.IsSuccessStatusCode)
		{
			_logger.LogError("{error}", httpResponse.RequestMessage?.ToString());
			_logger.LogError("Claim public_key fail due to: status_code: {code}, msg: {error}",
				httpResponse.StatusCode,
				await httpResponse.Content.ReadAsStringAsync(cancellationToken));
			throw new Exception("Service Unavailable");
		}

		var responseData =
			await httpResponse.Content.ReadFromJsonAsync<HDBankApiPublicKeyResponseModel>(
				cancellationToken: cancellationToken);

		var publicKey = responseData?.Data.Key;

		if (string.IsNullOrEmpty(publicKey))
		{
			throw new NullReferenceException();
		}

		_logger.LogInformation("Claim public_key success: {pubKey}", publicKey);
		_credential.SetRsaPublicKey(publicKey);
	}
}
