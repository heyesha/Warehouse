namespace Warehouse.Domain.Enums;

public enum ErrorCodes
{
    WarehouseNotFound = 0,
    WarehouseAlreadyExists = 1,
    
    ProductNotFound = 10,
    ProductAlreadyExists = 11,
    
    SuppliesNotFound = 20,
    
    EmployeeAlreadyExists = 30,
    EmployeeNotFound = 31
}