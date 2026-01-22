using FluentAssertions;
using Moq;
using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Application.Products.UpdateProduct;
using ProductHub.Domain.Products;

namespace ProductHub.Application.Tests.Products.UpdateProduct;

public class UpdateProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly UpdateProductService _updateProductService;

    public UpdateProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _updateProductService = new UpdateProductService(_productRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidCommand_ShouldUpdateProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var existingProduct = new Product("Arroz", 10.00m, 50);
        var command = new UpdateProductCommand(productId, "Arroz Integral", 12.00m, 30);
        var cancellationToken = CancellationToken.None;

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId, cancellationToken))
            .ReturnsAsync(existingProduct);

        // Act
        await _updateProductService.ExecuteAsync(command, cancellationToken);

        // Assert
        existingProduct.Name.Should().Be("Arroz Integral");
        existingProduct.Price.Should().Be(12.00m);
        existingProduct.Quantity.Should().Be(30);

        _productRepositoryMock.Verify(
            x => x.UpdateAsync(existingProduct, cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonExistentProduct_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new UpdateProductCommand(productId, "Arroz", 10.00m, 50);
        var cancellationToken = CancellationToken.None;

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId, cancellationToken))
            .ReturnsAsync((Product?)null);

        // Act
        Func<Task> act = async () => await _updateProductService.ExecuteAsync(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Product with ID {productId} not found");
    }
}