using System.Text.Json.Serialization;

namespace PikaServer.Common.HdBankHttpApiBase;

public class HdBankRemoteApiRequest<TData>
{
	public HdBankRemoteApiRequest(TData data)
	{
		Data = data;
		Request = new AuditRequest();
	}

	[JsonPropertyName("data")] public TData Data { get; set; }

	[JsonPropertyName("request")] public AuditRequest Request { get; set; }

	public class AuditRequest
	{
		[JsonPropertyName("requestId")] public string RequestId { get; set; } = "a7ea23df-7468-439d-9b12-26eb4a760901";

		[JsonPropertyName("requestTime")] public string RequestTime { get; set; } = "1667200102200";
	}
}
