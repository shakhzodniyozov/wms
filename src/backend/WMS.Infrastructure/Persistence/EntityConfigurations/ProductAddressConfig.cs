using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain;

namespace WMS.Infrastructure;

public class ProductAddressConfig : IEntityTypeConfiguration<ProductAddress>
{
    public void Configure(EntityTypeBuilder<ProductAddress> builder)
    {
        builder.ToTable("product_addresses");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductAddresses)
                .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.Address)
                .WithMany(x => x.ProductAddresses)
                .HasForeignKey(x => x.AddressId);
    }
}
