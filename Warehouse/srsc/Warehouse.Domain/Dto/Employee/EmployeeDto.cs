namespace Warehouse.Domain.Dto.Employee;

public record EmployeeDto(long Id, string Name, string Email, string Phone, long WarehouseId, int? TotalPoints, int? CountOfTasks);