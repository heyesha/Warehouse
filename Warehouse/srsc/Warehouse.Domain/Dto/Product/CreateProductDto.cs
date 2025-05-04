namespace Warehouse.Domain.Dto.Product;

public record CreateProductDto(string Name, string Category, string ArticleNumber, decimal Price);