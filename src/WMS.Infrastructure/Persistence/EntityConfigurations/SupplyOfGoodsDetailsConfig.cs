using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain;

namespace WMS.Infrastructure;

public class SupplyOfGoodsDetailsConfig : IEntityTypeConfiguration<SupplyOfGoodsDetails>
{
    public void Configure(EntityTypeBuilder<SupplyOfGoodsDetails> builder)
    {
        builder.ToTable("supply_of_goods_details");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.SupplyOfGoods)
                .WithMany(x => x.SupplyOfGoodsDetails)
                .HasForeignKey(x => x.SupplyOfGoodsId);

        builder.HasOne(x => x.Product)
                .WithMany(x => x.SupplyOfGoodsDetails)
                .HasForeignKey(x => x.ProductId);
    }
}
