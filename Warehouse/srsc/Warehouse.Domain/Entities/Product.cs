using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class Product : IAuditable, IEntityId<long>
{
    public long Id { get; set; }
    
    public required string Name { get; set; }
    
    public string Category { get; set; }
    
    public required string ArticleNumber { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}