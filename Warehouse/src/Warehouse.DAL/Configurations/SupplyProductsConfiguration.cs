using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.DAL.Configurations;

public class SupplyProductsConfiguration : IEntityTypeConfiguration<SupplyProducts>
{
    public void Configure(EntityTypeBuilder<SupplyProducts> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}