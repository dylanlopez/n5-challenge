using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;

namespace N5.Application;

public static class ApplicationDependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(Assembly.GetExecutingAssembly());
		return services;
	}
}
