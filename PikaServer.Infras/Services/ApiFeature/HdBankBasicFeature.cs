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

		EnsureHdBankApiResponseHelper.ThrowIfNull(response);

		if (!response!.Response.IsResponseCodeSuccess())
		{
			_logger.LogError("Get balance fail due to: {message}", PikaJsonConvert.SerializeObject(response.Response));
			return null;
		}

		return double.Parse(response.Data.Amount);
	}

	public async Task<AuditResponse> TransferAsync(double amount, string description, string fromAccNo, string toAccNo,
		CancellationToken cancellationToken = default)
	{
		// prepare body
		var reqBody = new HdBankRemoteApiRequest<RemoteTransferRequestData>(new RemoteTransferRequestData(
			amount, description, fromAccNo, toAccNo));

		// send req
		var httpResponse = await _httpClient.PostAsync("transfer", reqBody.AsJsonContent(), cancellationToken);

		// parse result
		var response =
			await httpResponse.Content.ReadFromJsonAsync<HdBankRemoteApiResponse<RemoteTransferResponseData>>(
				cancellationToken: cancellationToken);

		EnsureHdBankApiResponseHelper.ThrowIfNull(response);
		ProcessTransferLog(response, reqBody);

		return response!.Response;
	}

	public async Task<IEnumerable<RemoteTransactionHistoryResponseData.Transaction>> GetTransactionHistAsync(
		string accountNo, DateTime fromDate, DateTime toDate,
		CancellationToken cancellationToken = default)
	{
		// prepare body
		var fromDateFormat = fromDate.ToString("ddMMyyyy");
		var toDateFormat = toDate.ToString("ddMMyyyy");
		var reqBody = new HdBankRemoteApiRequest<RemoteTransactionHistoryRequestData>(
			new RemoteTransactionHistoryRequestData(
				accountNo, fromDateFormat, toDateFormat));

		// send req
		var httpResponse = await _httpClient.PostAsync("tranhis", reqBody.AsJsonContent(), cancellationToken);

		var response = await httpResponse.Content
			.ReadFromJsonAsync<HdBankRemoteApiResponse<RemoteTransactionHistoryResponseData>>(
				cancellationToken: cancellationToken);

		EnsureHdBankApiResponseHelper.ThrowIfNull(response);

		return response!.Data.TransactionList;
	}

	private void ProcessTransferLog(HdBankRemoteApiResponse<RemoteTransferResponseData>? response,
		HdBankRemoteApiRequest<RemoteTransferRequestData> reqBody)
	{
		if (!response!.Response.IsResponseCodeSuccess())
		{
			_logger.LogError("Transfer fail due to: {message}", PikaJsonConvert.SerializeObject(response.Response));
		}

		_logger.LogInformation("Transfer request success: {content}", PikaJsonConvert.SerializeObject(reqBody));
	}
}
