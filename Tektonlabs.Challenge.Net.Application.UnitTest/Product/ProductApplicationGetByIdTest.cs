using Bogus;
using FluentAssertions;
using Moq;
using Tektonlabs.Challenge.Net.Application.Discount;
using Tektonlabs.Challenge.Net.Application.Products.GetProduct;
using Tektonlabs.Challenge.Net.Domain.Products;
using Tektonlabs.Challenge.Net.Domain.Status;

namespace Tektonlabs.Challenge.Net.Application.UnitTest.Product;

public class GetProductQueryHandlerTest
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IStatusRepository> _mockStatusRepository;
    private readonly Mock<IDiscountService> _mockDiscountService;
    private readonly Faker _faker = new();

    public GetProductQueryHandlerTest()
    {
        _mockProductRepository = new();
        _mockStatusRepository = new();
        _mockDiscountService = new();
    }

    [Fact]
    public async Task Handle_ShouldReturnProductWhenFound()
    {
        // Arrange
        var cancellationToken = new CancellationToken();

        var existingProduct = Domain.Products.Product.Create(
           Name.Create(_faker.Commerce.ProductName()).Value,
           Stock.Create(_faker.Random.Int(1,1000)).Value,
           Description.Create(_faker.Lorem.Sentence()).Value,
           Price.Create(_faker.Random.Decimal(1,10000)).Value,
           //Discount.Create(_faker.Random.Decimal(0, 100)).Value,
           DateTime.UtcNow
           //new PriceService()
       ).Value;

        var expectedResponse = new ProductResponse(
            existingProduct.Id,
            existingProduct.Name.Value,
            "Active", 
            existingProduct.Stock.Value, 
            existingProduct.Description.Value,
            existingProduct.Price.Value,
            10,
            existingProduct.Price.Value - (existingProduct.Price.Value * 10 / 100),
            existingProduct.CreateDate,
            existingProduct.LastUpdateDate
        );

        _mockProductRepository.Setup(x => x.GetByIdAsync(existingProduct.Id, cancellationToken))
            .ReturnsAsync(existingProduct);

        _mockStatusRepository.Setup(r => r.GetByKeyAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Status.Create(1, "Active").Value);

        _mockDiscountService.Setup(d => d.GetDiscountByProductId(It.IsAny<Guid>()))
            .ReturnsAsync(10); 

        var handler = new GetProductQueryHandler(_mockProductRepository.Object,_mockStatusRepository.Object,_mockDiscountService.Object);
        var query = new GetProductQuery(existingProduct.Id);

        // Act
        var result = await handler.Handle(query, cancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedResponse, options =>
            options.Excluding(p => p.CreateDate)
            .Excluding(p => p.LastUpdateDate));
    }

}