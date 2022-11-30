namespace PikaServer.Api.Schemas;

public class TransferRequest
{
	public TransferRequest(double amount, string description, string fromAccountNo, string toAccountNo)
	{
		Amount = amount;
		Description = description;
		FromAccountNo = fromAccountNo;
		ToAccountNo = toAccountNo;
	}

	public double Amount { get; init; }
	public string Description { get; init; }
	public string FromAccountNo { get; init; }

	public string ToAccountNo { get; init; }
}
