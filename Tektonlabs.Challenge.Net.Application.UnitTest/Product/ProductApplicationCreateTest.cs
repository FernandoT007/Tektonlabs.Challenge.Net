using Bogus;
using FluentAssertions;
using Moq;
using Tektonlabs.Challenge.Net.Application.Abstractions.Clock;
using Tektonlabs.Challenge.Net.Application.Products.CreateProduct;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;
using Xunit;

namespace Tektonlabs.Challenge.Net.Application.UnitTest.Product
{
    public class TestProductApplicationCreateTest
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
        protected readonly Faker _faker = new();

        public TestProductApplicationCreateTest()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        }

        [Fact]
        public async Task Handle_ShouldCreateProductAndReturnSuccess()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            var command = new CreateProductCommand(
                _faker.Commerce.ProductName(),
                _faker.Random.Int(1, 1000),
                _faker.Commerce.ProductDescription(),
                _faker.Random.Decimal(1, 10000)
            //    _faker.Random.Int(1, 100)
            );

            // Act
            var handler = new CreateProductCommandHandler(
                _mockProductRepository.Object,
                _mockUnitOfWork.Object,
                new PriceService(),
                _mockDateTimeProvider.Object
            );
            var result = await handler.Handle(command, cancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), cancellationToken), Times.Never); 
            _mockUnitOfWork.Verify(x => x.SaveChangeAsnyc(cancellationToken), Times.Once);
        }
    }
}
