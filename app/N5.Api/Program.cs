using Confluent.Kafka;
using N5.Application;
using N5.Infrastructure;
using Nest;

namespace N5.Api;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var configuration = builder.Configuration;

		// Add services to the container.

		builder.Services.AddApplication();
		builder.Services.AddInfrastructure(configuration);
		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowAllOrigins",
				builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
		});

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		//builder.Services.AddSingleton<IElasticClient>(sp =>
		//{
		//	var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
		//		.DefaultIndex("permissions");

		//	return new ElasticClient(settings);
		//});

		//builder.Services.AddSingleton<IProducer<Null, string>>(sp =>
		//{
		//	var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
		//	return new ProducerBuilder<Null, string>(config).Build();
		//});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();
		app.UseCors("AllowAllOrigins");
		app.UseAuthorization();
		app.MapControllers();
		app.Run();
	}
}
