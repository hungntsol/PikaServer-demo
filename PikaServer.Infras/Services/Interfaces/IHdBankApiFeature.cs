using PikaServer.Domain.Entities;

namespace PikaServer.Infras.Services.Interfaces;

public interface IHdBankApiFeature
{
	Task<string> RegisterAccountAsync(Account account, string password, CancellationToken cancellationToken = default);
}
