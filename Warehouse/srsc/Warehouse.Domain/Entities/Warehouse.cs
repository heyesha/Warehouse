using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class Warehouse : IAuditable, IEntityId<long>
{
    public long Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Address { get; set; }
    
    public required string Type { get; set; }
    
    public List<Product> Products { get; set; }
    
    public List<Supply> Supplies { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}