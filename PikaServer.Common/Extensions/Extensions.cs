using System.Text;
using Newtonsoft.Json;

namespace PikaServer.Common.Extensions;

public static class Extensions
{
	public static StringContent AsJson(this object obj)
	{
		return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
	}
}