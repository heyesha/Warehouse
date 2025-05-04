using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.DAL.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Domain.Entities.Warehouse>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Warehouse> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Type).IsRequired().HasMaxLength(50);

        builder.HasMany<Supply>(x => x.Supplies)
            .WithOne(x => x.Warehouse)
            .HasForeignKey(x => x.WarehouseId)
            .HasPrincipalKey(x => x.Id);

        builder.HasData(new List<Domain.Entities.Warehouse>()
        {
            new Domain.Entities.Warehouse()
            {
                Id = 1,
                Address = "Borisova 3",
                CreatedAt = DateTime.UtcNow,
                Name = "First",
                Type = "Freeze"
            },
            new Domain.Entities.Warehouse()
            {
                Id = 2,
                Address = "inter 2",
                CreatedAt = DateTime.UtcNow,
                Name = "Second",
                Type = "Freeze"
            },
            new Domain.Entities.Warehouse()
            {
                Id = 3,
                Address = "Tolb 2",
                CreatedAt = DateTime.UtcNow,
                Name = "Third",
                Type = "Garbage"
            }
        });
    }
}