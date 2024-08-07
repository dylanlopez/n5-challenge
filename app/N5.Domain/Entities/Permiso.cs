namespace N5.Domain.Entities;

public class Permiso
{
	public int Id { get; set; }
	public string EmpleadoNombre { get; set; }
	public string EmpleadoApellido { get; set; }
	public DateTime FechaPermiso { get; set; }
	public int TipoPermisoId { get; set; }
}
