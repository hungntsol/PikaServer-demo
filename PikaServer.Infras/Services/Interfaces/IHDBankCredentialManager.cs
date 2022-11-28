namespace PikaServer.Infras.Services.Interfaces;

public interface IHDBankCredentialManager
{
	Task<string> GetPublicKeyAsync(CancellationToken cancellationToken = default);

	string GetPublicKey();

	Task ClaimPublicKeyAsync(CancellationToken cancellationToken = default);
	//Task RefreshPublicKey();
	//Task<string> GetTokenAsync();
}
