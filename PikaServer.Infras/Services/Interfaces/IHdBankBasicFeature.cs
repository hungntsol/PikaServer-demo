using PikaServer.Common.HdBankHttpApiBase;
using PikaServer.Infras.HdBankHttpDataSchemas;

namespace PikaServer.Infras.Services.Interfaces;

public interface IHdBankBasicFeature
{
	Task<double?> GetBalanceAsync(string accountNo, CancellationToken cancellationToken = default);

	Task<AuditResponse> TransferAsync(double amount, string description, string fromAccNo, string toAccNo,
		CancellationToken cancellationToken = default);

	Task<IEnumerable<RemoteTransactionHistoryResponseData.Transaction>> GetTransactionHistAsync(string accountNo,
		DateTime fromDate,
		DateTime toDate, CancellationToken cancellationToken = default);
}
