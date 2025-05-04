namespace Warehouse.Domain.Dto.Product;

public record InventoryDto(long WarehouseId, long ProductId, int Amount);