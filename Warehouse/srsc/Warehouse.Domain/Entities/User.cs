using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class User : IAuditable, IEntityId<long>
{
    public long Id { get; set; }
    
    public required string Login { get; set; }
    
    public required string Password { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}