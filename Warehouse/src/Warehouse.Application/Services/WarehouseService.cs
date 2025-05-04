using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Resources;
using Warehouse.Domain.Dto;
using Warehouse.Domain.Dto.Product;
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
        var warehouses = await _warehouseRepository.GetAll()
            .Select(x => new WarehouseDto(x.Id, x.Name, x.Address, x.Type))
            .ToArrayAsync();

        if (warehouses.Length == 0)
        {
            return new CollectionResult<WarehouseDto>()
            {
                ErrorMessage = ErrorMessage.WarehouseNotFound,
                ErrorCode = (int)ErrorCodes.WarehouseNotFound
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
        var warehouseDto = _warehouseRepository.GetAll()
            .AsEnumerable()
            .Select(x => new WarehouseDto(x.Id, x.Name, x.Address, x.Type))
            .FirstOrDefault(w => w.Id == id);

        if (warehouseDto == null)
        {
            return Task.FromResult(new BaseResult<WarehouseDto>()
            {
                ErrorMessage = ErrorMessage.WarehouseNotFound,
                ErrorCode = (int)ErrorCodes.WarehouseNotFound
            });
        }

        return Task.FromResult(new BaseResult<WarehouseDto>()
        {
            Data = warehouseDto,
        });
    }

    public async Task<BaseResult<WarehouseDto>> CreateWarehouseAsync(CreateWarehouseDto warehouseDto)
    {
        var warehouse = await _warehouseRepository.GetAll().FirstOrDefaultAsync(w => w.Name == warehouseDto.Name);

        if (warehouse != null)
        {
            return new BaseResult<WarehouseDto>()
            {
                ErrorMessage = ErrorMessage.WarehouseAlreadyExists,
                ErrorCode = (int)ErrorCodes.WarehouseAlreadyExists
            };
        }

        warehouse = new Domain.Entities.Warehouse()
        {
            Name = warehouseDto.Name,
            Address = warehouseDto.Address,
            Type = warehouseDto.Type
        };
        await _warehouseRepository.CreateAsync(warehouse);

        return new BaseResult<WarehouseDto>()
        {
            Data = new WarehouseDto(warehouse.Id, warehouse.Name, warehouse.Address, warehouse.Type)
        };
    }

    public async Task<BaseResult<WarehouseDto>> UpdateWarehouseAsync(UpdateWarehouseDto warehouseDto)
    {
        var warehouse = await _warehouseRepository.GetAll().FirstOrDefaultAsync(w => w.Id == warehouseDto.Id);

        if (warehouse == null)
        {
            return new BaseResult<WarehouseDto>()
            {
                ErrorMessage = ErrorMessage.WarehouseNotFound,
                ErrorCode = (int)ErrorCodes.WarehouseNotFound
            };
        }
        
        warehouse.Name = warehouseDto.Name;
        warehouse.Address = warehouseDto.Addres;
        warehouse.Type = warehouseDto.Type;
        
        var updatedWarehouse = _warehouseRepository.Update(warehouse);
        await _warehouseRepository.SaveChangesAsync();

        return new BaseResult<WarehouseDto>()
        {
            Data = new WarehouseDto(updatedWarehouse.Id, updatedWarehouse.Name, updatedWarehouse.Address,
                updatedWarehouse.Type)
        };
    }

    public async Task<BaseResult<WarehouseDto>> DeleteWarehouseAsync(long id)
    {
        var warehouse = await _warehouseRepository.GetAll().FirstOrDefaultAsync(w => w.Id == id);
        if (warehouse == null)
        {
            return new BaseResult<WarehouseDto>()
            {
                ErrorMessage = ErrorMessage.WarehouseNotFound,
                ErrorCode = (int)ErrorCodes.WarehouseNotFound
            };
        }
        
        _warehouseRepository.Remove(warehouse);
        await _warehouseRepository.SaveChangesAsync();

        return new BaseResult<WarehouseDto>()
        {
            Data = new WarehouseDto(warehouse.Id, warehouse.Name, warehouse.Address, warehouse.Type)
        };
    }
}