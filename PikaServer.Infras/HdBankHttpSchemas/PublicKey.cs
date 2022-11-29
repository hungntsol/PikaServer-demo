using System.Text.Json.Serialization;

namespace PikaServer.Infras.HdBankHttpSchemas;

public class PublicKeyResponseData
{
	public PublicKeyResponseData(string key)
	{
		Key = key;
	}

	[JsonPropertyName("key")] public string Key { get; set; }
}
