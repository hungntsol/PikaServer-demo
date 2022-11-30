namespace PikaServer.Infras.RemoteApiDataSpec;

public class RemoteChangePasswordRequestData : RemoteLoginAccountRequestData
{
	public RemoteChangePasswordRequestData(string credential, string key) : base(credential, key)
	{
	}
}
