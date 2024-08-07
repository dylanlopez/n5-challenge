using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace N5.Infrastructure;

using Application.Interfaces;
using Application.Interfaces.Repositories;
using Persistence;
using Repositories;

public static class InfrastructureDependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration)
	{
		//var con = Configuration.GetConnectionString("DefaultConnection");
		services.AddDbContext<N5DbContext>(options =>
			options.UseSqlServer(
				Configuration.GetConnectionString("DefaultConnection"),
				sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
			)
		);

		//Repositories
		services.AddTransient<IUnitOfWork, UnitOfWork>();
		services.AddTransient<IPermisoRepository, PermisoRepository>();

		return services;
	}
}
