using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Domain.Products;

namespace ProductHub.Application.Products.CreateProduct;

public class CreateProductService
{
  private readonly IProductRepository _productRepository;

  public CreateProductService(IProductRepository productRepository)
  {
    _productRepository = productRepository;
  }

  public async Task<Guid> ExecuteAsync(
      CreateProductCommand command,
      CancellationToken cancellationToken)
  {
    var product = new Product(
        command.Name,
        command.Price,
        command.Quantity);

    await _productRepository.AddAsync(product, cancellationToken);

    return product.Id;
  }
}
