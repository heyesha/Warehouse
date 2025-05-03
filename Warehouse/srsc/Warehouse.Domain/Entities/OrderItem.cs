using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class OrderItem : IEntityId<long>
{
    public long Id { get; set; }
    
    public int OrderId { get; set; }
    
    public Order Order { get; set; }
    
    public long ProductId { get; set; }
    
    public Product Product { get; set; }
    
    public int Amount { get; set; }
}