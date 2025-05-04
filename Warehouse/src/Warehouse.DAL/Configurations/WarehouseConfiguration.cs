using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Warehouse.DAL.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Domain.Entities.Warehouse>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Warehouse> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Type).IsRequired().HasMaxLength(50);
        
        
    }
}