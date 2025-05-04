using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class Supply : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    
    public long WarehouseId { get; set; }
    
    public Warehouse Warehouse { get; set; }
    
    public string? Destination { get; set; }
    
    public string? Description { get; set; }
    
    public List<Product> Products { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}