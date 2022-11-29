using PikaServer.Infras.HdBankHttpSchemas;

namespace PikaServer.Infras.Services.Interfaces;

public interface IHdBankAuthService
{
	Task<OAuth2Response> OAuth2Async(CancellationToken cancellationToken = default);
}
