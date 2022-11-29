namespace PikaServer.Infras.Services.Interfaces;

public interface IHdBankCredentialManager
{
	string GetPublicKey();

	Task ClaimPublicKeyAsync(CancellationToken cancellationToken = default);

	string CreateCredential(string username, string password);
}
