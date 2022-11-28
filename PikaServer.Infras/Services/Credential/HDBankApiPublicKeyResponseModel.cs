using System.Text.Json.Serialization;
using PikaServer.Common.HttpMessageObjects;

namespace PikaServer.Infras.Services.Credential;

public class HDBankApiPublicKeyResponseModel : HDBankApiResponseBase<HDBankApiPublicKeyResponseModel.DataModel>
{
	public HDBankApiPublicKeyResponseModel(DataModel data, AuditResponse response) : base(data, response)
	{
	}

	public class DataModel
	{
		public DataModel(string key)
		{
			Key = key;
		}

		[JsonPropertyName("key")] public string Key { get; set; }
	}
}
