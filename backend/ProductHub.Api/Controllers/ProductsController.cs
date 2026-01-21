using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductHub.Api.Models.Products;
using ProductHub.Application.Products.CreateProduct;
using ProductHub.Application.Products.DeleteProduct;
using ProductHub.Application.Products.GetProductById;
using ProductHub.Application.Products.GetProducts;
using ProductHub.Application.Products.RegisterSale;
using ProductHub.Application.Products.UpdateProduct;

namespace ProductHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/products")]
public class ProductsController(
    GetProductsService getProductsService,
    GetProductByIdService getProductByIdService,
    CreateProductService createProductService,
    RegisterSaleService registerSaleService,
    UpdateProductService updateProductService,
    DeleteProductService deleteProductService) : ControllerBase
{
    private readonly GetProductsService _getProductsService = getProductsService;
    private readonly GetProductByIdService _getProductByIdService = getProductByIdService;
    private readonly CreateProductService _createProductService = createProductService;
    private readonly RegisterSaleService _registerSaleService = registerSaleService;
    private readonly UpdateProductService _updateProductService = updateProductService;
    private readonly DeleteProductService _deleteProductService = deleteProductService;

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _getProductsService
            .ExecuteAsync(new GetProductsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _getProductByIdService
            .ExecuteAsync(new GetProductByIdQuery(id), cancellationToken);

        if (result == null)
            return NotFound(new { message = "Product not found" });

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Price,
            request.Quantity);

        var productId = await _createProductService.ExecuteAsync(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = productId }, new { id = productId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        await _updateProductService.ExecuteAsync(command with { Id = id }, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _deleteProductService.ExecuteAsync(new DeleteProductCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/sales")]
    public async Task<IActionResult> RegisterSale(
        Guid id,
        [FromBody] RegisterSaleRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterSaleCommand(id, request.Quantity);
        await _registerSaleService.ExecuteAsync(command, cancellationToken);
        return NoContent();
    }
}

public record RegisterSaleRequest(int Quantity);