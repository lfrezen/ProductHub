namespace ProductHub.Application.Products.RegisterSale;

public record RegisterSaleCommand(
    Guid ProductId,
    int Quantity
);
