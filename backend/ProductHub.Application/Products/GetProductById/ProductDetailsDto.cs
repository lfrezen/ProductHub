namespace ProductHub.Application.Products.GetProductById;

public record ProductDetailsDto(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity,
    bool IsOutOfStock,
    DateTime? LastSaleDate
);
