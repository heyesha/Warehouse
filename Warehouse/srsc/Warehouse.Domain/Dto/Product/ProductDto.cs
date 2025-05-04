namespace Warehouse.Domain.Dto.Product;

public record ProductDto(long Id, string Name, string? Category, string ArticleNumber, decimal Price);