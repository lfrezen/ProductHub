namespace ProductHub.Application.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    decimal Price,
    int Quantity
);
