using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PikaServer.Persistence.Context;

namespace PikaServer.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection UseDbPersistence(this IServiceCollection services, string connStr)
	{
		services.AddDbContext<AppDbContext>(opts =>
			opts.UseSqlServer(connStr,
				sqlOpt => sqlOpt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))
		);

		return services;
	}
}
