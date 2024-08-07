using MediatR;

namespace N5.Domain.Dtos.Commands.Request;

public class UpdatePermisoCommandRequest : IRequest
{
	public int Id { get; set; }
	public string EmpleadoNombre { get; set; }
	public string EmpleadoApellido { get; set; }
	public DateTime FechaPermiso { get; set; }
	public int TipoPermisoId { get; set; }
}
