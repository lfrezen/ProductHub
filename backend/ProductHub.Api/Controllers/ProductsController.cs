using Microsoft.AspNetCore.Mvc;
using ProductHub.Application.Products.CreateProduct;
using ProductHub.Application.Products.RegisterSale;

namespace ProductHub.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
  private readonly CreateProductService _createProductService;
  private readonly RegisterSaleService _registerSaleService;

  public ProductsController(
      CreateProductService createProductService,
      RegisterSaleService registerSaleService)
  {
    _createProductService = createProductService;
    _registerSaleService = registerSaleService;
  }

  [HttpPost]
  public async Task<IActionResult> Create(
      CreateProductCommand command,
      CancellationToken cancellationToken)
  {
    var productId = await _createProductService.ExecuteAsync(command, cancellationToken);
    return CreatedAtAction(nameof(Create), new { id = productId }, null);
  }

  [HttpPost("{id}/sales")]
  public async Task<IActionResult> RegisterSale(
      Guid id,
      [FromBody] int quantity,
      CancellationToken cancellationToken)
  {
    var command = new RegisterSaleCommand(id, quantity);
    await _registerSaleService.ExecuteAsync(command, cancellationToken);
    return NoContent();
  }
}
