using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using PikaServer.Common.Extensions;
using PikaServer.Common.HdBankHttpApiBase;
using PikaServer.Common.Utils;
using PikaServer.Infras.Constants;
using PikaServer.Infras.HdBankHttpDataSchemas;
using PikaServer.Infras.Helpers;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Infras.Services.ApiFeature;

public class HdBankBasicFeature : IHdBankBasicFeature
{
	private readonly HttpClient _httpClient;
	private readonly ILogger<HdBankBasicFeature> _logger;

	public HdBankBasicFeature(ILogger<HdBankBasicFeature> logger, IHttpClientFactory httpClientFactory)
	{
		_logger = logger;
		_httpClient = httpClientFactory.CreateClient(HttpClientNameConstants.HdBank);
	}

	public async Task<double?> GetBalanceAsync(string accountNo, CancellationToken cancellationToken = default)
	{
		// prepare body
		var body = new HdBankRemoteApiRequest<RemoteBalanceRequestData>(new RemoteBalanceRequestData(accountNo));

		// send req
		var httpResponse = await _httpClient.PostAsync("balance", body.AsJsonContent(), cancellationToken);

		var response =
			await httpResponse.Content.ReadFromJsonAsync<HdBankRemoteApiResponse<RemoteBalanceResponseData>>(
				cancellationToken: cancellationToken);

		EnsureResponseDataHelper.ThrowIfNull(response);

		if (!response!.Response.IsResponseCodeSuccess())
		{
			_logger.LogError("Get balance fail due to: {message}", PikaJsonConvert.SerializeObject(response.Response));
			return null;
		}

		return double.Parse(response.Data.Amount);
	}
}
