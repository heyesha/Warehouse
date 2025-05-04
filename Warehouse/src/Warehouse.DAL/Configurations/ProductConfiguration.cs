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

        builder.HasData(new List<Product>()
        {
            new Product()
            {
                Id = 3,
                Name = "Apple",
                Category = "Fruit",
                ArticleNumber = "12345",
                Price = (decimal)9.99,
                CreatedAt = DateTime.UtcNow
            }
        });
    }
}