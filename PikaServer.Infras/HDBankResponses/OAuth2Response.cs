using System.Text.Json.Serialization;

namespace PikaServer.Infras.HDBankResponses;

public class OAuth2Response
{
	[JsonPropertyName("id_token")]
	public string IdToken { get; set; }
	[JsonPropertyName("access_token")]
	public string AccessToken { get; set; }
	[JsonPropertyName("expires_in")]
	public int ExpiresIn { get; set; }
	[JsonPropertyName("token_type")]
	public string TokenType { get; set; }

	public OAuth2Response(string idToken, string accessToken, int expiresIn, string tokenType)
	{
		IdToken = idToken;
		AccessToken = accessToken;
		ExpiresIn = expiresIn;
		TokenType = tokenType;
	}
}