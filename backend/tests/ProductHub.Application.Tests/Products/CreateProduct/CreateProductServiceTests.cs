using FluentAssertions;
using Moq;
using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Application.Products.CreateProduct;
using ProductHub.Domain.Products;

namespace ProductHub.Application.Tests.Products.CreateProduct;

public class CreateProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly CreateProductService _createProductService;

    public CreateProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _createProductService = new CreateProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidCommand_ShouldCreateProduct()
    {
        // Arrange
        var command = new CreateProductCommand("Arroz", 10.50m, 100);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _createProductService.ExecuteAsync(command, cancellationToken);

        // Assert
        result.Should().NotBeEmpty();
        _productRepositoryMock.Verify(
            x => x.AddAsync(It.Is<Product>(p =>
                p.Name == "Arroz" &&
                p.Price == 10.50m &&
                p.Quantity == 100
            ), cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnProductId()
    {
        // Arrange
        var command = new CreateProductCommand("Feijão", 8.00m, 50);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _createProductService.ExecuteAsync(command, cancellationToken);

        // Assert
        result.Should().NotBeEmpty();
    }
}