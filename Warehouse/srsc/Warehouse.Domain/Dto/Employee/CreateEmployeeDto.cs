namespace Warehouse.Domain.Dto.Employee;

public record CreateEmployeeDto(string Name, string Email, string Phone, long WarehouseId);