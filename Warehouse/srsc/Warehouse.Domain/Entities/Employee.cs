using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class Employee : IEntityId<long>
{
    public long Id { get; set; }
    
    
    public required string Name { get; set; }

    public required string Email { get; set; }
    
    public required string Phone { get; set; }

    public int TotalPoints { get; set; } = 0;

    public int CountOfTasks { get; set; } = 0;
    
    public long WarehouseId { get; set; }
    
    public Warehouse Warehouse { get; set; }
    public List<Achievement> Achievements { get; set; }
}