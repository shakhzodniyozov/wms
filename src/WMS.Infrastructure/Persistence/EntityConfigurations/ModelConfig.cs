using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain;


namespace WMS.Infrastructure;

public class ModelConfig : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("models");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ModelName)
                .HasMaxLength(32);

        builder.HasOne(x => x.Manufacturer)
                .WithMany(x => x.Models)
                .HasForeignKey(x => x.ManufacturerId);

        builder.HasMany(x => x.Products)
                .WithMany(x => x.Models);
    }
}
