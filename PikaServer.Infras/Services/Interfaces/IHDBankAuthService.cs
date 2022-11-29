using PikaServer.Domain.Entities;
using PikaServer.Infras.HdBankHttpDataSchemas;

namespace PikaServer.Infras.Services.Interfaces;

public interface IHdBankAuthService
{
	Task<RemoteOAuth2Response> OAuth2Async(CancellationToken cancellationToken = default);
	Task<string> RegisterAccountAsync(Account account, string password, CancellationToken cancellationToken = default);

	Task<RemoteLoginAccountResult> LoginAccountAsync(Account account, string password,
		CancellationToken cancellationToken = default);
	//Task<bool> ChangePassword(string username, string password, CancellationToken cancellationToken = default);
}
