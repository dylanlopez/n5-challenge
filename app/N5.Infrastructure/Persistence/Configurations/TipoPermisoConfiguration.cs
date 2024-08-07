using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace N5.Infrastructure.Persistence.Configurations;

using Domain.Entities;

public class TipoPermisoConfiguration : IEntityTypeConfiguration<TipoPermiso>
{
	public void Configure(EntityTypeBuilder<TipoPermiso> builder)
	{
		builder.ToTable("TipoPermiso");

		builder.HasKey(x => x.Id);

		builder.Property(b => b.Id)
			.HasColumnName("id");

		builder.Property(b => b.Descripcion)
			.HasColumnName("descripcion")
			.HasMaxLength(50);
	}
}
