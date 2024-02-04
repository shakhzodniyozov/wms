using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain;

namespace WMS.Infrastructure;

public class EngineConfig : IEntityTypeConfiguration<Engine>
{
    public void Configure(EntityTypeBuilder<Engine> builder)
    {
        builder.ToTable("engines");

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Products).WithMany(x => x.Engines);
    }
}
