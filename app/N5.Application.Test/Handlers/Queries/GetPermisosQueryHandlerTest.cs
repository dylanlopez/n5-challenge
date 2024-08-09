using FluentAssertions;
using Moq;
using N5.Application.Handlers.Queries;
using N5.Application.Interfaces.Repositories;
using N5.Domain.Dtos.Queries.Request;
using N5.Domain.Entities;

namespace N5.Application.Test.Handlers.Queries;

public class GetPermisosQueryHandlerTest
{
	private readonly Mock<IPermisoRepository> _repositoryMock;
	private readonly GetPermisosQueryHandler _handler;

	public GetPermisosQueryHandlerTest()
	{
		_repositoryMock = new Mock<IPermisoRepository>();
		_handler = new GetPermisosQueryHandler(_repositoryMock.Object);
	}

	[Fact]
	public async Task Handle_ShouldReturnListOfPermisos()
	{
		// Arrange
		var permisos = new List<Permiso>
		{
			new Permiso { Id = 1, EmpleadoNombre = "Juan", EmpleadoApellido = "Perez", FechaPermiso = DateTime.Now, TipoPermisoId = 1 },
			new Permiso { Id = 2, EmpleadoNombre = "Carlos", EmpleadoApellido = "Ramirez", FechaPermiso = DateTime.Now.AddDays(1), TipoPermisoId = 2 }
		};

		_repositoryMock.Setup(r => r.GetPermisos())
			.ReturnsAsync(permisos);

		var query = new GetPermisosQueryRequest();

		// Act
		var result = await _handler.Handle(query, CancellationToken.None);

		// Assert
		result.Should().BeEquivalentTo(permisos);
		_repositoryMock.Verify(r => r.GetPermisos(), Times.Once);
	}
}
