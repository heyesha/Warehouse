using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class SupplyProducts : IEntityId<long>
{
    public long Id { get; set; }
    
    public long ProductId { get; set; }
    
    
    public long SupplyId { get; set; }
    
    
    public int Amount { get; set; }
}