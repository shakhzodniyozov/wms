using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain;

namespace WMS.Infrastructure;

public class ImageConfig : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("images");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Product)
                .WithOne(x => x.Image)
                .HasForeignKey<Image>(x => x.ProductId);
    }
}
