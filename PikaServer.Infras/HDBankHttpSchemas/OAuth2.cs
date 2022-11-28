﻿using System.Text.Json.Serialization;

namespace PikaServer.Infras.HDBankHttpSchemas;

public class OAuth2TokenRequest
{
	public OAuth2TokenRequest(string clientId, string refreshToken, string grantType)
	{
		ClientId = clientId;
		RefreshToken = refreshToken;
		GrantType = grantType;
	}

	public string ClientId { get; set; }
	public string GrantType { get; set; }
	public string RefreshToken { get; set; }
}

public class OAuth2Response
{
	public OAuth2Response(string idToken, string accessToken, int expiresIn, string tokenType)
	{
		IdToken = idToken;
		AccessToken = accessToken;
		ExpiresIn = expiresIn;
		TokenType = tokenType;
	}

	[JsonPropertyName("id_token")] public string IdToken { get; set; }

	[JsonPropertyName("access_token")] public string AccessToken { get; set; }

	[JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }

	[JsonPropertyName("token_type")] public string TokenType { get; set; }
}