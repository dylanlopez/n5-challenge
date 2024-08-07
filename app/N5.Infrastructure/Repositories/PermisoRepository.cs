using Microsoft.EntityFrameworkCore;

namespace N5.Infrastructure.Repositories;

using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence;

public class PermisoRepository : IPermisoRepository
{
	private readonly N5DbContext _context;

	public PermisoRepository(N5DbContext context)
	{
		_context = context;
	}

	public async Task<List<Permiso>> GetPermisos() => await _context.Permisos.ToListAsync();

	public async Task<Permiso> GetPermisoById(int id) => await _context.Permisos.FindAsync(id);

	public async Task AddPermiso(Permiso permission)
	{
		_context.Permisos.Add(permission);
		await _context.SaveChangesAsync();
	}

	public async Task UpdatePermiso(Permiso permission)
	{
		_context.Permisos.Update(permission);
		await _context.SaveChangesAsync();
	}
}
