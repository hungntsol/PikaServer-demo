using PikaServer.Infras.HDBankResponses;

namespace PikaServer.Infras.Services;

public interface IHDBankAuthService
{
	Task<OAuth2Response> OAuth2(string clientId, string grantType, string refreshToken);
}

public class HDBankAuthService
{
	
}