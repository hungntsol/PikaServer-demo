namespace PikaServer.Infras.AppSettings;

public class HDBankApiAppSetting
{
	public string AuthServer { get; set; }
	public string BaseUrl { get; set; }
	public string ClientId { get; set; }
	public string RefreshToken { get; set; }
	public string ApiKey { get; set; }

	public HDBankApiAppSetting(string authServer, string baseUrl, string clientId, string refreshToken, string apiKey)
	{
		AuthServer = authServer;
		BaseUrl = baseUrl;
		ClientId = clientId;
		RefreshToken = refreshToken;
		ApiKey = apiKey;
	}
}