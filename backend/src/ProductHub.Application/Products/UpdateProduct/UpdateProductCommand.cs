namespace ProductHub.Application.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    int Quantity
);
