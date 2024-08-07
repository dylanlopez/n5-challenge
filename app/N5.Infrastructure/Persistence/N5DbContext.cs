using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace N5.Infrastructure.Persistence;

using Domain.Entities;

public class N5DbContext : DbContext
{
	public virtual DbSet<Permiso> Permisos { get; set; }
	public virtual DbSet<TipoPermiso> TiposPermisos { get; set; }

	public N5DbContext(DbContextOptions<N5DbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		base.OnModelCreating(modelBuilder);
	}
}
