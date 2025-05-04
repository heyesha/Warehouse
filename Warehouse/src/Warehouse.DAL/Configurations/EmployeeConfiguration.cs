using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Entities;

namespace Warehouse.DAL.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(20);
        builder.Property(x => x.WarehouseId).IsRequired();
        builder.Property(x => x.TotalPoints).HasDefaultValue(0);
        builder.Property(x => x.CountOfTasks).HasDefaultValue(0);
        
        builder.HasMany(x => x.Achievements)
            .WithMany(x => x.Employees)
            .UsingEntity<EmployeeAchievement>(
                l => l.HasOne<Achievement>().WithMany().HasForeignKey(x => x.AchievementId),
                l => l.HasOne<Employee>().WithMany().HasForeignKey(x => x.EmployeeId)
            );
        
        builder.HasData(new List<Employee>()
        {
            new Employee()
            {
                Id = 1,
                Name = "John Doe",
                Email = "johndoe@gmail.com",
                Phone = "555-555-5555",
                WarehouseId = 1,
                TotalPoints = 100,
                CountOfTasks = 1
            },
            new Employee()
            {
                Id = 2,
                Name = "Rodion Kondrashin",
                Email = "konrod@gmail.com",
                Phone = "5287045",
                WarehouseId = 1,
                TotalPoints = 1337,
                CountOfTasks = 53
            },
            new Employee()
            {
                Id = 3,
                Name = "Domenico Heyesha",
                Email = "domalp@gmail.com",
                Phone = "88005553535",
                WarehouseId = 1,
                TotalPoints = 4,
                CountOfTasks = 0
            }
        });
    }
}