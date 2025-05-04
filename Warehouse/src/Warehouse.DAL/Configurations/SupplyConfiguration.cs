using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.DAL.Configurations;

public class SupplyConfiguration : IEntityTypeConfiguration<Supply>
{
    public void Configure(EntityTypeBuilder<Supply> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}