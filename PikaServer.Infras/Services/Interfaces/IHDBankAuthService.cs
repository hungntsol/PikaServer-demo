using PikaServer.Infras.HDBankHttpSchemas;

namespace PikaServer.Infras.Services.Interfaces;

public interface IHDBankAuthService
{
	Task<OAuth2Response> OAuth2Async(CancellationToken cancellationToken = default);
}
