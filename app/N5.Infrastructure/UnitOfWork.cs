namespace N5.Infrastructure;

using Application.Interfaces;
using Application.Interfaces.Repositories;
using Persistence;
using Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly N5DbContext _context;

	public UnitOfWork(N5DbContext context)
	{
		_context = context;
		Permisos = new PermisoRepository(_context);
	}

	public IPermisoRepository Permisos { get; }

	public async Task CompleteAsync()
	{
		await _context.SaveChangesAsync();
	}
}
