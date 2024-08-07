using Moq;
using N5.Application.Handlers.Commands;
using N5.Application.Interfaces;
using N5.Domain.Dtos.Commands.Request;
using N5.Domain.Entities;

namespace N5.Application.Test.Handlers.Commands;

public class AddPermisoCommandHandlerTest
{
	[Fact]
	public async Task RequestPermission_ShouldAddPermission()
	{
		// Arrange
		var mockUnitOfWork = new Mock<IUnitOfWork>();
		//mockUnitOfWork.Setup(s => s.Permisos(), It.IsAny<>())
		var handler = new AddPermisoCommandHandler(mockUnitOfWork.Object, null, null);

		// Act
		await handler.Handle(new AddPermisoCommandRequest
		{
			EmpleadoNombre = "Name", 
			EmpleadoApellido = "LastName", 
			FechaPermiso = DateTime.Now, 
			TipoPermisoId = 1
		}, CancellationToken.None);

		// Assert
		mockUnitOfWork.Verify(uow => uow.Permisos.AddPermiso(It.IsAny<Permiso>()), Times.Once);
	}
}
