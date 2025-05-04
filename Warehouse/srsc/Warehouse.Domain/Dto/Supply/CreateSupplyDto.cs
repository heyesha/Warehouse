using Warehouse.Domain.Dto.SupplyProduct;

namespace Warehouse.Domain.Dto.Supply;

public record CreateSupplyDto(long WarehouseId, List<SupplyProductDto> Products, string? Description, string? Destination);