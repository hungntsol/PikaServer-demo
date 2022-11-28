using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PikaServer.Common.Utils;

public static class JsonOptions
{
	public static JsonSerializerSettings Default => BuildDefault();

	public static JsonSerializerSettings SnakeCase => BuildSnakeCase();

	public static JsonSerializerSettings BuildSnakeCase()
	{
		return new JsonSerializerSettings
		{
			ContractResolver = new DefaultContractResolver
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			}
		};
	}

	public static JsonSerializerSettings BuildDefault()
	{
		return new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver()
		};
	}
}