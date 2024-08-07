using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace N5.Infrastructure.Persistence.Configurations;

using Domain.Entities;

public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
{
	public void Configure(EntityTypeBuilder<Permiso> builder)
	{
		builder.ToTable("Permiso");

		builder.HasKey(x => x.Id);

		builder.Property(b => b.Id)
			.HasColumnName("id");

		builder.Property(b => b.EmpleadoNombre)
			.HasColumnName("NombreEmpleado")
			.HasMaxLength(25);

		builder.Property(b => b.EmpleadoApellido)
			.HasColumnName("ApellidoEmpleado")
			.HasMaxLength(25);

		builder.Property(b => b.FechaPermiso)
			.HasColumnName("FechaPermiso");

		builder.Property(b => b.TipoPermisoId)
			.HasColumnName("TipoPermisoId");
	}
}
