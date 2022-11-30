using PikaServer.Common.HdBankHttpApiBase;

namespace PikaServer.Infras.Services.Interfaces;

public interface IHdBankBasicFeature
{
	Task<double?> GetBalanceAsync(string accountNo, CancellationToken cancellationToken = default);

	Task<AuditResponse> TransferAsync(double amount, string description, string fromAccNo, string toAccNo,
		CancellationToken cancellationToken = default);
}
