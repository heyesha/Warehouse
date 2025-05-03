namespace Warehouse.Domain.Interfaces.Database;

public interface IStateSaveChanges
{
    Task<int> SaveChangesAsync();
}