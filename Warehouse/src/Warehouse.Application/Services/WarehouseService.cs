using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Resources;
using Warehouse.Domain.Dto;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Result;

namespace Warehouse.Application.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IBaseRepository<Domain.Entities.Warehouse> _warehouseRepository;

    public WarehouseService(IBaseRepository<Domain.Entities.Warehouse> warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<CollectionResult<WarehouseDto>> GetWarehousesAsync()
    {
        WarehouseDto[] warehouses;

        warehouses = await _warehouseRepository.GetAll()
            .Select(x => new WarehouseDto(x.Id, x.Name, x.Address, x.Type))
            .ToArrayAsync();

        if (warehouses.Length == 0)
        {
            return new CollectionResult<WarehouseDto>()
            {
                ErrorMessage = ErrorMessage.WarehouesNotFound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound
            };
        }

        return new CollectionResult<WarehouseDto>()
        {
            Data = warehouses,
            Count = warehouses.Length
        };
    }

    public Task<BaseResult<WarehouseDto>> GetWarehouseByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<WarehouseDto>> CreateWarehouseAsync(CreateWarehouseDto warehouse)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<WarehouseDto>> UpdateWarehouseAsync(UpdateWarehouseDto warehouse)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult<WarehouseDto>> DeleteWarehouseAsync(long id)
    {
        throw new NotImplementedException();
    }
}