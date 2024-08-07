using Confluent.Kafka;
using MediatR;
using Nest;
using System.Text.Json;

namespace N5.Application.Handlers.Commands;

using Domain.Dtos.Commands.Request;
using Domain.Entities;
using Interfaces;

public class AddPermisoCommandHandler : IRequestHandler<AddPermisoCommandRequest>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IElasticClient _elasticClient;
	private readonly IProducer<Null, string> _producer;

	public AddPermisoCommandHandler(
		IUnitOfWork unitOfWork, 
		IElasticClient elasticClient, 
		IProducer<Null, string> producer)
	{
		_unitOfWork = unitOfWork;
		_elasticClient = elasticClient;
		_producer = producer;
	}

	public async Task<Unit> Handle(AddPermisoCommandRequest request, CancellationToken cancellationToken)
	{
		//crear entidad
		var permiso = new Permiso();
		permiso.EmpleadoNombre = request.EmpleadoNombre;
		permiso.EmpleadoApellido = request.EmpleadoApellido;
		permiso.FechaPermiso = request.FechaPermiso;
		permiso.TipoPermisoId = request.TipoPermisoId;

		await _unitOfWork.Permisos.AddPermiso(permiso);
		await _unitOfWork.CompleteAsync();

		await _elasticClient.IndexDocumentAsync(permiso);

		var message = JsonSerializer.Serialize(new { Id = Guid.NewGuid(), Operation = "request", Permission = permiso });
		await _producer.ProduceAsync("request_permissions", new Message<Null, string> { Value = message });

		return Unit.Value;
	}
}
