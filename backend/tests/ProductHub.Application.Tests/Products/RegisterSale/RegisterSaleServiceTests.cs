using FluentAssertions;
using Moq;
using ProductHub.Application.Abstractions.Repositories;
using ProductHub.Application.Products.RegisterSale;
using ProductHub.Domain.Products;
using ProductHub.Domain.Sales;

namespace ProductHub.Application.Tests.Products.RegisterSale;

public class RegisterSaleServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly RegisterSaleService _sut;

    public RegisterSaleServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _sut = new RegisterSaleService(
            _productRepositoryMock.Object,
            _saleRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidCommand_ShouldRegisterSale()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product("Arroz", 10.00m, 50);
        var command = new RegisterSaleCommand(productId, 10);
        var cancellationToken = CancellationToken.None;

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId, cancellationToken))
            .ReturnsAsync(product);

        // Act
        await _sut.ExecuteAsync(command, cancellationToken);

        // Assert
        product.Quantity.Should().Be(40);
        product.LastSaleDate.Should().NotBeNull();

        _productRepositoryMock.Verify(
            x => x.UpdateAsync(product, cancellationToken),
            Times.Once);

        _saleRepositoryMock.Verify(
            x => x.AddAsync(It.Is<Sale>(s =>
                s.ProductId == productId &&
                s.Quantity == 10
            ), cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonExistentProduct_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new RegisterSaleCommand(productId, 10);
        var cancellationToken = CancellationToken.None;

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId, cancellationToken))
            .ReturnsAsync((Product?)null);

        // Act
        Func<Task> act = async () => await _sut.ExecuteAsync(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Product with ID {productId} not found");
    }

    [Fact]
    public async Task ExecuteAsync_WithInsufficientStock_ShouldThrowException()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product("Arroz", 10.00m, 5);
        var command = new RegisterSaleCommand(productId, 10);
        var cancellationToken = CancellationToken.None;

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(productId, cancellationToken))
            .ReturnsAsync(product);

        // Act
        Func<Task> act = async () => await _sut.ExecuteAsync(command, cancellationToken);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Insufficient product quantity.");
    }
}