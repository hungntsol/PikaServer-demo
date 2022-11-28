using System.Text.Json.Serialization;

namespace PikaServer.Common.HttpMessageObjects;

public abstract class HDBankApiResponseBase<TResponse>
{
	protected HDBankApiResponseBase(TResponse data, AuditResponse response)
	{
		Data = data;
		Response = response;
	}

	[JsonPropertyName("data")] public TResponse Data { get; set; }

	[JsonPropertyName("response")] public AuditResponse Response { get; set; }

	public class AuditResponse
	{
		[JsonPropertyName("responseId")] public string ResponseId { get; set; } = null!;

		[JsonPropertyName("responseCode")] public string ResponseCode { get; set; } = null!;

		[JsonPropertyName("responseMessage")] public string ResponseMessage { get; set; } = null!;

		[JsonPropertyName("responseTime")] public string ResponseTime { get; set; } = null!;
	}
}
