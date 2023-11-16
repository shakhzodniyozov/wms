using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain;

namespace WMS.Infrastructure;

public class ManufacturerConfig : IEntityTypeConfiguration<Manufacturer>
{
    public void Configure(EntityTypeBuilder<Manufacturer> builder)
    {
        builder.ToTable("manufacturers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(32);
    }
}
