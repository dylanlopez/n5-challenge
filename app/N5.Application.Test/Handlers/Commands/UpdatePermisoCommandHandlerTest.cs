using Confluent.Kafka;
using FluentAssertions;
using MediatR;
using Moq;
using N5.Application.Handlers.Commands;
using N5.Application.Interfaces.Repositories;
using N5.Domain.Dtos.Commands.Request;
using N5.Domain.Entities;

namespace N5.Application.Test.Handlers.Commands;

public class UpdatePermisoCommandHandlerTest
{
	private readonly Mock<IPermisoRepository> _repositoryMock;
	private readonly Mock<IProducer<Null, string>> _producerMock;
	private readonly UpdatePermisoCommandHandler _handler;

	public UpdatePermisoCommandHandlerTest()
	{
		_repositoryMock = new Mock<IPermisoRepository>();
		_producerMock = new Mock<IProducer<Null, string>>();
		_handler = new UpdatePermisoCommandHandler(_repositoryMock.Object, _producerMock.Object);
	}

	[Fact]
	public async Task Handle_ShouldUpdatePermisoAndProduceMessage()
	{
		// Arrange
		var permiso = new Permiso
		{
			Id = 1,
			EmpleadoNombre = "Juan",
			EmpleadoApellido = "Perez",
			FechaPermiso = DateTime.Now,
			TipoPermisoId = 1
		};

		var command = new UpdatePermisoCommandRequest
		{
			Id = permiso.Id,
			EmpleadoNombre = "Carlos",
			EmpleadoApellido = "Ramirez",
			FechaPermiso = permiso.FechaPermiso.AddDays(1),
			TipoPermisoId = 2
		};

		_repositoryMock.Setup(r => r.GetPermisoById(command.Id))
			.ReturnsAsync(permiso);

		_repositoryMock.Setup(r => r.UpdatePermiso(It.IsAny<Permiso>()))
			.Returns(Task.CompletedTask);

		_producerMock.Setup(p => p.ProduceAsync("modify_permissions", It.IsAny<Message<Null, string>>(), It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult(new DeliveryResult<Null, string>()));

		// Act
		var result = await _handler.Handle(command, CancellationToken.None);

		// Assert
		_repositoryMock.Verify(r => r.UpdatePermiso(It.Is<Permiso>(p =>
			p.Id == command.Id &&
			p.EmpleadoNombre == command.EmpleadoNombre &&
			p.EmpleadoApellido == command.EmpleadoApellido &&
			p.FechaPermiso == command.FechaPermiso &&
			p.TipoPermisoId == command.TipoPermisoId
		)), Times.Once);

		result.Should().Be(Unit.Value);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenPermisoNotFound()
	{
		// Arrange
		var command = new UpdatePermisoCommandRequest
		{
			Id = 1,
			EmpleadoNombre = "Carlos",
			EmpleadoApellido = "Ramirez",
			FechaPermiso = DateTime.Now,
			TipoPermisoId = 2
		};

		_repositoryMock.Setup(r => r.GetPermisoById(command.Id))
			.ReturnsAsync((Permiso)null);

		// Act
		Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

		// Assert
		await act.Should().ThrowAsync<Exception>().WithMessage("Permission not found");

		_repositoryMock.Verify(r => r.UpdatePermiso(It.IsAny<Permiso>()), Times.Never);
	}
}
