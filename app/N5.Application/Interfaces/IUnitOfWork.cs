namespace N5.Application.Interfaces;

using Repositories;

public interface IUnitOfWork
{
	IPermisoRepository Permisos { get; }
	Task CompleteAsync();
}
