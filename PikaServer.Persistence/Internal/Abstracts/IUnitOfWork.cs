using PikaServer.Persistence.Repositories;

namespace PikaServer.Persistence.Internal.Abstracts;

public interface IUnitOfWork : IDisposable
{
	IAccountRepository Account { get; }
	Task<int> Commit();
}
