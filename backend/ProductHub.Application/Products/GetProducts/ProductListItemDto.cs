namespace ProductHub.Application.Products.GetProducts;

public record ProductListItemDto(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity,
    bool IsOutOfStock
);
