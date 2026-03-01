using MaxAuto.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaxAuto.WebApi.Infrastructure.Context.Configurations;

public sealed class MechanicConfiguration : IEntityTypeConfiguration<Mechanic>
{
    public void Configure(EntityTypeBuilder<Mechanic> b)
    {
        b.ToTable("mechanics");

        b.HasKey(x => x.Id);

        b.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        b.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        b.Property(x => x.CreatedAt).IsRequired();
        b.Property(x => x.UpdatedAt).IsRequired();
    }
}
