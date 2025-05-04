using Warehouse.Domain.Dto.Supply;
using Warehouse.Domain.Result;

namespace Warehouse.Domain.Interfaces.Services;

/// <summary>
/// Сервис поставок
/// </summary>
public interface ISupplyService
{
    /// <summary>
    /// Получение поставки по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<SupplyDto>> GetSupplyByIdAsync(long id);
    
    /// <summary>
    /// Создание поставки
    /// </summary>
    /// <param name="supplyDto"></param>
    /// <returns></returns>
    Task<BaseResult<SupplyDto>> CreateSupplyAsync(CreateSupplyDto supplyDto);
    
    /// <summary>
    /// Получение всех поставок склада по его ID
    /// </summary>
    /// <param name="warehouseId"></param>
    /// <returns></returns>
    Task<BaseResult<SupplyDto>> GetSuppliesByWarehouse(long warehouseId);
}