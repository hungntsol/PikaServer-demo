using System.Text.Json.Serialization;

namespace PikaServer.Infras.HdBankHttpDataSchemas;

public class RemoteBalanceRequestData
{
	public RemoteBalanceRequestData(string accountNo)
	{
		AccountNo = accountNo;
	}

	[JsonPropertyName("acctNo")] public string AccountNo { get; set; }
}

public class RemoteBalanceResponseData
{
	public RemoteBalanceResponseData(string amount)
	{
		Amount = amount;
	}

	[JsonPropertyName("amount")] public string Amount { get; set; }
}
