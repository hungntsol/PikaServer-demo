namespace PikaServer.Infras.AppSettings;

public class HDBankApiSetting
{
	public HDBankApiSetting(string authServer, string baseUrl, string clientId, string refreshToken, string apiKey)
	{
		AuthServer = authServer;
		BaseUrl = baseUrl;
		ClientId = clientId;
		RefreshToken = refreshToken;
		ApiKey = apiKey;
	}

	public string AuthServer { get; set; }
	public string BaseUrl { get; set; }
	public string ClientId { get; set; }
	public string RefreshToken { get; set; }
	public string ApiKey { get; set; }
}