using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Warehouse.Domain.Entities;

namespace Warehouse.DAL.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Category).HasMaxLength(50);
        builder.Property(x => x.ArticleNumber).IsRequired().HasMaxLength(16);
        builder.Property(x => x.Price).IsRequired();

        builder.HasMany(x => x.Warehouses)
            .WithMany(x => x.Products)
            .UsingEntity<ProductWarehouse>(
                l => l.HasOne<Domain.Entities.Warehouse>().WithMany().HasForeignKey(x => x.WarehouseId),
                l => l.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId)
            );

        builder.HasMany(x => x.Supplies)
            .WithMany(x => x.Products)
            .UsingEntity<SupplyProducts>(
                l => l.HasOne<Supply>().WithMany().HasForeignKey(x => x.SupplyId),
                l => l.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId)
            );

        builder.HasData(new List<Product>()
        {
            new Product()
            {
                Id = 4,
                Name = "Apple",
                Category = "Fruit",
                ArticleNumber = "12345",
                Price = (decimal)9.99,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 0
            },
            new Product()
            {
                Id = 2,
                Name = "Banana",
                Category = "Fruit",
                ArticleNumber = "adfg",
                Price = (decimal)4.5,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 0
            }
            , 
            new Product()
            {
                Id = 1,
                Name = "Phone",
                Category = "Device",
                ArticleNumber = "fgxv",
                Price = (decimal)199.00,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 0
            }
        });
    }
}