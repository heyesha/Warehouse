using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Resources;
using Warehouse.Domain.Dto.Product;
using Warehouse.Domain.Dto.Supply;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Result;

namespace Warehouse.Application.Services;

public class SupplyService : ISupplyService
{
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IBaseRepository<Supply> _supplyRepository;
    private readonly IBaseRepository<SupplyProducts> _supplyProductsRepository;
    private readonly IBaseRepository<Domain.Entities.Warehouse> _warehouseRepository;
    private readonly IBaseRepository<ProductWarehouse> _productWarehouseRepository;

    public SupplyService(
        IBaseRepository<Product> productRepository, 
        IBaseRepository<Supply> supplyRepository, 
        IBaseRepository<SupplyProducts> supplyProductsRepository, 
        IBaseRepository<Domain.Entities.Warehouse> warehouseRepository, 
        IBaseRepository<ProductWarehouse> productWarehouseRepository)
    {
        _productRepository = productRepository;
        _supplyRepository = supplyRepository;
        _supplyProductsRepository = supplyProductsRepository;
        _warehouseRepository = warehouseRepository;
        _productWarehouseRepository = productWarehouseRepository;
    }

    public Task<BaseResult<SupplyDto>> GetSupplyByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResult<SupplyDto>> CreateSupplyAsync(CreateSupplyDto supplyDto)
    {
        var warehouse = await _warehouseRepository.GetAll().FirstOrDefaultAsync(x => x.Id == supplyDto.WarehouseId);
        if (warehouse == null)
        {
            return new BaseResult<SupplyDto>()
            {
                ErrorMessage = ErrorMessage.WarehouseNotFound,
                ErrorCode = (int)ErrorCodes.WarehouseNotFound
            };
        }

        var supply = new Supply()
        {
            WarehouseId = warehouse.Id,
            Destination = supplyDto.Destination,
            Description = supplyDto.Description
        };
        await _supplyRepository.CreateAsync(supply);
        
        foreach (var productDto in supplyDto.Products)
        {
            var product = _productRepository.GetAll().FirstOrDefault(x => x.Id == productDto.ProductId);
            if (product == null)
            {
                product = await _productRepository.CreateAsync(product);
            }

            var supplyProduct = new SupplyProducts()
            {
                SupplyId = supply.Id,
                ProductId = productDto.ProductId,
                Amount = productDto.Amount
            };
            await _supplyProductsRepository.CreateAsync(supplyProduct);
            
            await UpdateWarehouseInventory(new InventoryDto(supplyDto.WarehouseId, productDto.ProductId, productDto.Amount));
        }
        await _supplyRepository.SaveChangesAsync();
        return new BaseResult<SupplyDto>()
        {
            Data = new SupplyDto(supply.Id, supply.Destination, supply.Description),
        };
    }

    public async Task UpdateWarehouseInventory(InventoryDto inventoryDto)
    {
        var inventory = await _productWarehouseRepository
            .GetAll()
            .FirstOrDefaultAsync(x => x.ProductId == inventoryDto.ProductId && x.WarehouseId == inventoryDto.WarehouseId);
        if (inventory == null)
        {
            inventory = new ProductWarehouse()
            {
                ProductId = inventoryDto.ProductId,
                WarehouseId = inventoryDto.WarehouseId,
                Amount = inventoryDto.Amount
            };
        }
        else
        {
            inventory.Amount += inventoryDto.Amount;
        }
        _productWarehouseRepository.Update(inventory);
        await _productWarehouseRepository.SaveChangesAsync();
    }

    public async Task<BaseResult<SupplyDto>> GetSuppliesByWarehouse(long warehouseId)
    {
        var warehouse = await _warehouseRepository.GetAll()
            .FirstOrDefaultAsync(x => x.Id == warehouseId);
        if (warehouse == null)
        {
            return new BaseResult<SupplyDto>()
            {
                ErrorMessage = ErrorMessage.WarehouseNotFound,
                ErrorCode = (int)ErrorCodes.WarehouseNotFound
            };
        }

        var supplies = await _supplyRepository.GetAll()
            .Where(x => x.WarehouseId == warehouseId)
            .Select(x => new SupplyDto(x.Id, x.Description, x.Destination))
            .ToArrayAsync();

        if (supplies.Length == 0)
        {
            return new CollectionResult<SupplyDto>()
            {
                ErrorMessage = ErrorMessage.SuppliesNotFound,
                ErrorCode = (int)ErrorCodes.SuppliesNotFound
            }
        }
    }
}