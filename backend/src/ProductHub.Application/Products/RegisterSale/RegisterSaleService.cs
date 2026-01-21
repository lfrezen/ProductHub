using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Domain.Sales;

namespace ProductHub.Application.Products.RegisterSale;

public class RegisterSaleService
{
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;

    public RegisterSaleService(
        IProductRepository productRepository,
        ISaleRepository saleRepository)
    {
        _productRepository = productRepository;
        _saleRepository = saleRepository;
    }

    public async Task ExecuteAsync(
        RegisterSaleCommand command,
        CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.ProductId, cancellationToken);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {command.ProductId} not found");

        product.RegisterSale(command.Quantity);

        var sale = new Sale(command.ProductId, command.Quantity);

        await _productRepository.UpdateAsync(product, cancellationToken);
        await _saleRepository.AddAsync(sale, cancellationToken);
    }
}