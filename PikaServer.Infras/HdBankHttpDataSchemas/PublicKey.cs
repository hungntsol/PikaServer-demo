using System.Text.Json.Serialization;

namespace PikaServer.Infras.HdBankHttpDataSchemas;

public class RemotePublicKeyResponseData
{
	public RemotePublicKeyResponseData(string key)
	{
		Key = key;
	}

	[JsonPropertyName("key")] public string Key { get; set; }
}
