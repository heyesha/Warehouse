using Microsoft.EntityFrameworkCore;
using Warehouse.Application.Resources;
using Warehouse.Domain.Dto;
using Warehouse.Domain.Dto.Product;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Interfaces.Services;
using Warehouse.Domain.Result;

namespace Warehouse.Application.Services;

public class ProductService : IProductService
{
    private readonly IBaseRepository<Product> _productRepository;

    public ProductService(IBaseRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    /// <inheritdoc />
    public Task<BaseResult<ProductDto>> GetProductByIdAsync(long id)
    {
        var productDto = _productRepository.GetAll()
            .AsEnumerable()
            .Select(x => new ProductDto(x.Id, x.Name, x.Category, x.ArticleNumber, x.Price))
            .FirstOrDefault(w => w.Id == id);

        if (productDto == null)
        {
            return Task.FromResult(new BaseResult<ProductDto>()
            {
                ErrorMessage = ErrorMessage.ProductNotFound,
                ErrorCode = (int)ErrorCodes.ProductNotFound
            });
        }

        return Task.FromResult(new BaseResult<ProductDto>()
        {
            Data = productDto,
        });
    }

    /// <inheritdoc />
    public Task<BaseResult<ProductDto>> GetProductByNameAsync(ProductDto dto)
    {
        var productDto = _productRepository.GetAll()
            .AsEnumerable()
            .Select(x => new ProductDto(x.Id, x.Name, x.Category, x.ArticleNumber, x.Price))
            .FirstOrDefault(w => w.Name == dto.Name);

        if (productDto == null)
        {
            return Task.FromResult(new BaseResult<ProductDto>()
            {
                ErrorMessage = ErrorMessage.ProductNotFound,
                ErrorCode = (int)ErrorCodes.ProductNotFound
            });
        }

        return Task.FromResult(new BaseResult<ProductDto>()
        {
            Data = productDto,
        });
    }

    /// <inheritdoc />
    public async Task<BaseResult<ProductDto>> CreateProductAsync(CreateProductDto productDto)
    {
        var product = await _productRepository.GetAll().FirstOrDefaultAsync(w => w.Name == productDto.Name);

        if (product != null)
        {
            return new BaseResult<ProductDto>()
            {
                ErrorMessage = ErrorMessage.ProductAlreadyExists,
                ErrorCode = (int)ErrorCodes.ProductAlreadyExists
            };
        }

        product = new Product()
        {
            Name = productDto.Name,
            Category = productDto.Category,
            ArticleNumber = productDto.ArticleNumber,
            Price = productDto.Price,
        };
        await _productRepository.CreateAsync(product);

        return new BaseResult<ProductDto>()
        {
            Data = new ProductDto(product.Id, product.Name, product.Category, product.ArticleNumber, product.Price),
        };
    }

    /// <inheritdoc />
    public async Task<BaseResult<ProductDto>> UpdateProductAsync(ProductDto productDto)
    {
        var product = await _productRepository.GetAll().FirstOrDefaultAsync(w => w.Id == productDto.Id);

        if (product == null)
        {
            return new BaseResult<ProductDto>()
            {
                ErrorMessage = ErrorMessage.ProductNotFound,
                ErrorCode = (int)ErrorCodes.ProductNotFound
            };
        }
        
        product.Name = productDto.Name;
        product.Category = productDto.Category;
        product.ArticleNumber = productDto.ArticleNumber;
        product.Price = productDto.Price;
        
        var updatedWarehouse = _productRepository.Update(product);
        await _productRepository.SaveChangesAsync();

        return new BaseResult<ProductDto>()
        {
            Data = new ProductDto(product.Id, product.Name, product.Category, product.ArticleNumber, product.Price),
        };
    }

    /// <inheritdoc />
    public async Task<BaseResult<ProductDto>> DeleteProductAsync(long id)
    {
        var product = await _productRepository.GetAll().FirstOrDefaultAsync(w => w.Id == id);
        if (product == null)
        {
            return new BaseResult<ProductDto>()
            {
                ErrorMessage = ErrorMessage.ProductNotFound,
                ErrorCode = (int)ErrorCodes.ProductNotFound
            };
        }
        
        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync();

        return new BaseResult<ProductDto>()
        {
            Data = new ProductDto(product.Id, product.Name, product.Category, product.ArticleNumber, product.Price),
        };
    }
}