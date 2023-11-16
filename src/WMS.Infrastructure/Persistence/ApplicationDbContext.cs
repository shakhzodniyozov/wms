using Microsoft.EntityFrameworkCore;

namespace WMS.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfig());
        modelBuilder.ApplyConfiguration(new EmployeeConfig());
        modelBuilder.ApplyConfiguration(new ManufacturerConfig());
        modelBuilder.ApplyConfiguration(new ModelConfig());
        modelBuilder.ApplyConfiguration(new PriceConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());
        modelBuilder.ApplyConfiguration(new ProductAddressConfig());
        modelBuilder.ApplyConfiguration(new SupplyOfGoodsConfig());
        modelBuilder.ApplyConfiguration(new SupplyOfGoodsConfig());
    }
}
