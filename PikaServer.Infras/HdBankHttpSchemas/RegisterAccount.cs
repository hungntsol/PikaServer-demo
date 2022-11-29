using System.Text.Json.Serialization;
using PikaServer.Domain.Entities;

namespace PikaServer.Infras.HdBankHttpSchemas;

public class RegisterAccountRequestData
{
	public RegisterAccountRequestData(string credential, string email, string fullName, string identityNumber,
		string key, string phone)
	{
		Credential = credential;
		Email = email;
		FullName = fullName;
		IdentityNumber = identityNumber;
		Key = key;
		Phone = phone;
	}

	[JsonPropertyName("credential")] public string Credential { get; set; }

	[JsonPropertyName("email")] public string Email { get; set; }

	[JsonPropertyName("fullName")] public string FullName { get; set; }

	[JsonPropertyName("identityNumber")] public string IdentityNumber { get; set; }

	[JsonPropertyName("key")] public string Key { get; set; }

	[JsonPropertyName("phone")] public string Phone { get; set; }

	public static RegisterAccountRequestData Create(string credential, string key, Account account)
	{
		return new RegisterAccountRequestData(credential,
			account.Email,
			account.FullName,
			account.IdentityNumber, key,
			account.Phone);
	}
}

public class RegisterAccountResponseData
{
	public RegisterAccountResponseData(string userId)
	{
		UserId = userId;
	}

	[JsonPropertyName("userId")] public string UserId { get; set; }
}
