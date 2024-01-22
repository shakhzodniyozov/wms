using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain;

namespace WMS.Infrastructure;

public class PriceConfig : IEntityTypeConfiguration<Price>
{
    public void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.ToTable("prices");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Product)
                .WithMany(x => x.Prices)
                .HasForeignKey(x => x.ProductId);
    }
}
