using Warehouse.Domain.Dto;
using Warehouse.Domain.Result;

namespace Warehouse.Domain.Interfaces.Services;

/// <summary>
/// Сервис, отвечающий за работу с доменной частью склада
/// </summary>
public interface IWarehouseService
{
    /// <summary>
    /// Получение всех имеющихся складов
    /// </summary>
    /// <returns></returns>
    Task<CollectionResult<WarehouseDto>> GetWarehousesAsync();
    
    /// <summary>
    /// Получение склада по его идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<WarehouseDto>> GetWarehouseByIdAsync(long id);
    
    /// <summary>
    /// Создание склада с базовыми параметрами
    /// </summary>
    /// <param name="warehouse"></param>
    /// <returns></returns>
    Task<BaseResult<WarehouseDto>> CreateWarehouseAsync(CreateWarehouseDto warehouse);
    
    /// <summary>
    /// Обновление склада
    /// </summary>
    /// <param name="warehouse"></param>
    /// <returns></returns>
    Task<BaseResult<WarehouseDto>> UpdateWarehouseAsync(UpdateWarehouseDto warehouse);
    
    /// <summary>
    /// Удаление склада по его идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<WarehouseDto>> DeleteWarehouseAsync(long id);
}