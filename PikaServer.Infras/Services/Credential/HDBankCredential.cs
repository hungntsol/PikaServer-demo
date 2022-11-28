namespace PikaServer.Infras.Services.Credential;

public class HDBankCredential
{
	public string? IdToken { get; private set; }
	public string? AccessToken { get; private set; }
	public string? RsaPublicKey { get; private set; }

	public void SetIdToken(string idToken)
	{
		IdToken = idToken;
	}

	public void SetAccessToken(string accessToken)
	{
		AccessToken = accessToken;
	}

	public void SetRsaPublicKey(string pubKey)
	{
		RsaPublicKey = pubKey;
	}
}
