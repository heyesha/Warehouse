using Warehouse.Domain.Interfaces;

namespace Warehouse.Domain.Entities;

public class EmployeeAchievement : IEntityId<long>, IAuditable
{
    public long Id { get; set; }
    
    public long EmployeeId { get; set; }
    
    public long AchievementId { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    
    public long CreatedBy { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public long? UpdatedBy { get; set; }
}