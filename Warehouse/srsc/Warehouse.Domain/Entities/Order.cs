using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class Order : IAuditable, IEntityId<long>
{
    public long Id { get; set; }
    
    public User User { get; set; }
    
    public long UserId { get; set; }
    
    public List<OrderItem> OrderItems { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}