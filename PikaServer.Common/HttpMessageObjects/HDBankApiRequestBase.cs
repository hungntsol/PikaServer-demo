using System.Text.Json.Serialization;

namespace PikaServer.Common.HttpMessageObjects;

public abstract class HDBankApiRequestBase<TRequest>
{
	[JsonPropertyName("data")] public TRequest? Data { get; set; }

	[JsonPropertyName("request")] public AuditRequest Request { get; set; } = null!;

	public class AuditRequest
	{
		[JsonPropertyName("requestId")] public Guid RequestId { get; set; } = Guid.NewGuid();

		[JsonPropertyName("requestTime")]
		public long RequestTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();
	}
}
