using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WMS.Domain;

namespace WMS.Infrastructure;

public class SupplyOfGoodsConfig : IEntityTypeConfiguration<SupplyOfGoods>
{
    public void Configure(EntityTypeBuilder<SupplyOfGoods> builder)
    {
        builder.ToTable("supply_of_goods");

        builder.HasKey(x => x.Id);
    }
}
