namespace PikaServer.Infras.Services.Interfaces;

public interface IHdBankBasicFeature
{
	Task<double?> GetBalanceAsync(string accountNo, CancellationToken cancellationToken = default);
}
