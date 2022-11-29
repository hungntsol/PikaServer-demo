using PikaServer.Common.HdBankHttpApiBase;

namespace PikaServer.Infras.Helpers;

public static class EnsureResponseDataHelper
{
	public static void ThrowIfNull<T>(HdBankRemoteApiResponse<T>? response)
	{
		if (response is null)
		{
			throw new NullReferenceException();
		}
	}
}
