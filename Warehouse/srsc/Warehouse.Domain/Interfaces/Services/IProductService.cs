using Warehouse.Domain.Dto.Product;
using Warehouse.Domain.Result;

namespace Warehouse.Domain.Interfaces.Services;

public interface IProductService
{
    /// <summary>
    /// Получение товара по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ProductDto>> GetProductByIdAsync(long id);
    
    /// <summary>
    /// Получение товара по имени
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    Task<BaseResult<ProductDto>> GetProductByNameAsync(ProductDto productDto);
    
    /// <summary>
    /// Создание товара
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    Task<BaseResult<ProductDto>> CreateProductAsync(CreateProductDto productDto);
    
    /// <summary>
    /// Обновление товара
    /// </summary>
    /// <param name="productDto"></param>
    /// <returns></returns>
    Task<BaseResult<ProductDto>> UpdateProductAsync(ProductDto productDto);
    
    /// <summary>
    /// Удаление товара
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ProductDto>> DeleteProductAsync(long id);
}