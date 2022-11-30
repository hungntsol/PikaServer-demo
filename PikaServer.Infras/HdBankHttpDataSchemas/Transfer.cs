using System.Text.Json.Serialization;

namespace PikaServer.Infras.HdBankHttpDataSchemas;

public class RemoteTransferRequestData
{
	public RemoteTransferRequestData(double amount, string description, string fromAcct, string toAcct)
	{
		Amount = amount;
		Description = description;
		FromAcct = fromAcct;
		ToAcct = toAcct;
	}

	[JsonPropertyName("amount")] public double Amount { get; set; }

	[JsonPropertyName("description")] public string Description { get; set; }

	[JsonPropertyName("fromAcct")] public string FromAcct { get; set; }

	[JsonPropertyName("toAcct")] public string ToAcct { get; set; }
}

public class RemoteTransferResponseData
{
}
