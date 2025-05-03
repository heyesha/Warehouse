using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class Warehouse : IAuditable, IEntityId<long>
{
    public long Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Address { get; set; }
    
    public string Type { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}