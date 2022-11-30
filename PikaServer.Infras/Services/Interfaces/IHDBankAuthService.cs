using PikaServer.Domain.Entities;
using PikaServer.Infras.RemoteApiDataSpec;

namespace PikaServer.Infras.Services.Interfaces;

public interface IHdBankAuthService
{
	Task<RemoteOAuth2Response> OAuth2Async(CancellationToken cancellationToken = default);

	Task<string?> RegisterAccountAsync(Account account, string password, CancellationToken cancellationToken = default);

	Task<RemoteLoginAccountResult> LoginAccountAsync(Account account, string password,
		CancellationToken cancellationToken = default);

	Task<bool> ChangePassword(string username, string oldPassword, string newPassword,
		CancellationToken cancellationToken = default);
}
