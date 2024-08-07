using Confluent.Kafka;
using MediatR;
using System.Text.Json;

namespace N5.Application.Handlers.Commands;

using Domain.Dtos.Commands.Request;
using Interfaces.Repositories;

public class UpdatePermisoCommandHandler : IRequestHandler<UpdatePermisoCommandRequest>
{
	private readonly IPermisoRepository _repository;
	private readonly IProducer<Null, string> _producer;

	public UpdatePermisoCommandHandler(
		IPermisoRepository repository,
		IProducer<Null, string> producer
		)
	{
		_repository = repository;
		_producer = producer;
	}

	public async Task<Unit> Handle(UpdatePermisoCommandRequest request, CancellationToken cancellationToken)
	{
		var permiso = await _repository.GetPermisoById(request.Id);
		if (permiso == null)
		{
			throw new Exception("Permission not found");
		}

		permiso.EmpleadoNombre = request.EmpleadoNombre;
		permiso.EmpleadoApellido = request.EmpleadoApellido;

		permiso.FechaPermiso = request.FechaPermiso;
		permiso.TipoPermisoId = request.TipoPermisoId;

		await _repository.UpdatePermiso(permiso);

		var message = JsonSerializer.Serialize(permiso);
		await _producer.ProduceAsync("modify_permissions", new Message<Null, string> { Value = message });

		return Unit.Value;
	}
}
