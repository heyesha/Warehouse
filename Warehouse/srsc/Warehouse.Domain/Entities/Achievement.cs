using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class Achievement : IEntityId<long>
{
    public long Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public List<Employee> Employees { get; set; }
}