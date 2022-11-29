using System.Text.Json.Serialization;

namespace PikaServer.Infras.HdBankHttpDataSchemas;

public class RemoteLoginAccountRequestData
{
	public RemoteLoginAccountRequestData(string credential, string key)
	{
		Credential = credential;
		Key = key;
	}

	[JsonPropertyName("credential")] public string Credential { get; set; }

	[JsonPropertyName("key")] public string Key { get; set; }
}

public class RemoteLoginAccountResponseData
{
	public RemoteLoginAccountResponseData(string accountNo)
	{
		AccountNo = accountNo;
	}

	[JsonPropertyName("accountNo")] public string AccountNo { get; set; }
}

public class RemoteLoginAccountResult
{
	public string? AccountNo { get; set; }
	public LoginState State { get; set; }

	public bool IsSuccess => State == LoginState.Success;

	public static RemoteLoginAccountResult Success(string accountNo)
	{
		return new RemoteLoginAccountResult
		{
			AccountNo = accountNo,
			State = LoginState.Success
		};
	}

	public static RemoteLoginAccountResult Fail()
	{
		return new RemoteLoginAccountResult
		{
			AccountNo = null,
			State = LoginState.Failed
		};
	}
}

public enum LoginState
{
	Success,
	Failed
}
