using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace N5.Application;

public static class ApplicationDependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(Assembly.GetExecutingAssembly());

		// Registrar el producer en el contenedor de servicios
		services.AddSingleton<IProducer<Null, string>>(sp =>
		{
			var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
			return new ProducerBuilder<Null, string>(config).Build();
		});

		return services;
	}
}
