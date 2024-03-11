using Bogus;
using FluentAssertions;
using MediatR;
using Moq;
using Tektonlabs.Challenge.Net.Application.Abstractions.Clock;
using Tektonlabs.Challenge.Net.Application.Products.UpdateProduct;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;

namespace Tektonlabs.Challenge.Net.Application.UnitTest.Product;

public class TestProductApplicationUpdateTest
{
    UpdateProductCommandHandler handler;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
    protected readonly Faker _faker = new();
    protected readonly Guid idProduct = new Guid();

    public TestProductApplicationUpdateTest()
    {

        _mockProductRepository = new Mock<IProductRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();

        handler = new UpdateProductCommandHandler(
                _mockProductRepository.Object,
                _mockUnitOfWork.Object,
                //new PriceService(),
                _mockDateTimeProvider.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldUpdateProductAndReturnSuccess()
    { 
        // Arreglar
        var cancellationToken = new CancellationToken();

        var existingProduct = Domain.Products.Product.Create(
           Name.Create(_faker.Commerce.ProductName()).Value,
           Stock.Create(_faker.Random.Int(1, 1000)).Value,
           Description.Create(_faker.Lorem.Sentence()).Value,
           Price.Create(_faker.Random.Decimal(1,10000)).Value,
           DateTime.UtcNow
       ).Value;

        _mockProductRepository.Setup(x => x.GetByIdAsync(idProduct, cancellationToken))
            .ReturnsAsync(existingProduct);

        var updatedProductName = _faker.Commerce.ProductName();
        var updatedCommand = new UpdateProductCommand(
            idProduct,
            updatedProductName,
            _faker.PickRandom<ProductStatus>(),
            _faker.Random.Int(1, 1000),
            _faker.Commerce.ProductDescription(),
            _faker.Random.Decimal(1, 10000)
        );

        // Act    
        var result = await handler.Handle(updatedCommand, cancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _mockProductRepository.Verify(x => x.GetByIdAsync(idProduct, cancellationToken), Times.Once);
        existingProduct.Name.Value.Should().Be(updatedProductName);
        _mockUnitOfWork.Verify(x => x.SaveChangeAsnyc(cancellationToken), Times.Once);
    }
}