using System.Security.Cryptography;
using System.Text;
using PikaServer.Infras.Services.Credential;
using XC.RSAUtil;

namespace PikaServer.Infras.Helpers;

public class RsaCredentialHelper
{
	private const int DwKeySize = 1024;
	private static string? _xmlKey;

	private readonly HdBankCredential _credential;

	public RsaCredentialHelper(HdBankCredential credential)
	{
		_credential = credential;
	}

	public string CreateCredential(string username, string password)
	{
		var plainData = $"{{\"username\":\"{username}\",\"password\":\"{password}\"}}";
		var dataByteToEncrypt = Encoding.UTF8.GetBytes(plainData);

		var rsa = new RSACryptoServiceProvider(DwKeySize);
		rsa.FromXmlString(GetKeyXmlFormat());

		var encrypted = rsa.Encrypt(dataByteToEncrypt, false);
		return Convert.ToBase64String(encrypted);
	}

	private string GetKeyXmlFormat()
	{
		if (string.IsNullOrEmpty(_xmlKey))
		{
			_xmlKey = RsaKeyConvert.PublicKeyPemToXml(_credential.RsaPublicKey);
		}

		return _xmlKey;
	}
}
