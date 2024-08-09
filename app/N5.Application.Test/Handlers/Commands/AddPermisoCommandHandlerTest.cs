using Confluent.Kafka;
using FluentAssertions;
using MediatR;
using Moq;
using N5.Application.Handlers.Commands;
using N5.Application.Interfaces;
using N5.Domain.Dtos.Commands.Request;
using N5.Domain.Entities;

namespace N5.Application.Test.Handlers.Commands;

public class AddPermisoCommandHandlerTest
{
	private readonly Mock<IUnitOfWork> _unitOfWorkMock;
	private readonly Mock<IProducer<Null, string>> _producerMock;
	private readonly AddPermisoCommandHandler _handler;

	public AddPermisoCommandHandlerTest()
	{
		_unitOfWorkMock = new Mock<IUnitOfWork>();
		_producerMock = new Mock<IProducer<Null, string>>();
		_handler = new AddPermisoCommandHandler(_unitOfWorkMock.Object, _producerMock.Object);
	}

	[Fact]
	public async Task Handle_ShouldAddPermisoAndSendKafkaMessage_WhenValidRequest()
	{
		// Arrange
		var request = new AddPermisoCommandRequest
		{
			EmpleadoNombre = "John",
			EmpleadoApellido = "Doe",
			FechaPermiso = DateTime.Now,
			TipoPermisoId = 1
		};

		_unitOfWorkMock.Setup(uow => uow.Permisos.AddPermiso(It.IsAny<Permiso>()))
					   .Returns(Task.CompletedTask);

		_unitOfWorkMock.Setup(uow => uow.CompleteAsync())
					   .Returns(Task.CompletedTask);

		_producerMock.Setup(p => p.ProduceAsync(
			"request_permissions",
			It.IsAny<Message<Null, string>>(),
			It.IsAny<CancellationToken>()))
			.Returns(Task.FromResult((DeliveryResult<Null, string>)null));

		// Act
		var result = await _handler.Handle(request, CancellationToken.None);

		// Assert
		result.Should().Be(Unit.Value);

		_unitOfWorkMock.Verify(uow => uow.Permisos.AddPermiso(It.Is<Permiso>(p =>
			p.EmpleadoNombre == request.EmpleadoNombre &&
			p.EmpleadoApellido == request.EmpleadoApellido &&
			p.FechaPermiso == request.FechaPermiso &&
			p.TipoPermisoId == request.TipoPermisoId
		)), Times.Once);

		_unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
	}
}
