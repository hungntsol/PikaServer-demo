namespace PikaServer.Infras.Services;

public interface IHDBrankTokenManager
{
	Task<string> GetPublicKey();
	Task RefreshPublicKey();
	Task<string> GetTokenAsync();
}

public class HDBankTokenManager
{
	
}