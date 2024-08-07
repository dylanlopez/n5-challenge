namespace N5.Application.Interfaces.Repositories;

using Domain.Entities;

public interface IPermisoRepository
{
	Task<List<Permiso>> GetPermisos();
	Task<Permiso> GetPermisoById(int id);
	Task AddPermiso(Permiso permission);
	Task UpdatePermiso(Permiso permission);
}
