using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using PikaServer.Common.Extensions;
using PikaServer.Common.HdBankHttpApiBase;
using PikaServer.Common.Utils;
using PikaServer.Domain.Entities;
using PikaServer.Infras.Constants;
using PikaServer.Infras.HdBankHttpSchemas;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Infras.Services.ApiFeature;

public class HdBankApiFeature : IHdBankApiFeature
{
	private readonly IHdBankCredentialManager _credentialManager;
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ILogger<HdBankApiFeature> _logger;

	public HdBankApiFeature(IHdBankCredentialManager credentialManager,
		ILogger<HdBankApiFeature> logger,
		IHttpClientFactory httpClientFactory)
	{
		_credentialManager = credentialManager;
		_logger = logger;
		_httpClientFactory = httpClientFactory;
	}

	public async Task<string> RegisterAccountAsync(Account account, string password,
		CancellationToken cancellationToken = default)
	{
		var httpClient = _httpClientFactory.CreateClient(HttpClientConstants.HDBankClientName);
		var reqBody = new HdBankRemoteApiRequest<RegisterAccountRequestData>(RegisterAccountRequestData.Create(
			_credentialManager.CreateCredential(account.Email, password),
			_credentialManager.GetPublicKey(),
			account));

		var httpResponse =
			await httpClient.PostAsync("register", reqBody.AsJsonContent(), cancellationToken: cancellationToken);

		_logger.LogInformation("Response data {data}", await httpResponse.Content.ReadAsStringAsync(cancellationToken));

		var responseData =
			await httpResponse.Content.ReadFromJsonAsync<HdBankRemoteApiResponse<RegisterAccountResponseData>>(
				cancellationToken: cancellationToken);

		_logger.LogInformation("Register account success: {data}", PikaJsonConvert.SerializeObject(responseData!.Data));
		return responseData.Data.UserId;
	}
}
