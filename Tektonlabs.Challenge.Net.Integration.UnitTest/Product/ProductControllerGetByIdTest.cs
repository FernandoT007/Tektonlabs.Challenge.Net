using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tektonlabs.Challenge.Net.Api.Controllers.Product;
using Tektonlabs.Challenge.Net.Application.Products.GetProduct;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;
using Xunit;

namespace Tektonlabs.Challenge.Net.Api.Test.Controllers.Product
{
    public class ProductsController_GetProduct_Test
    {
        private readonly Mock<ISender> _mockSender;
        private readonly Faker _faker = new();

        public ProductsController_GetProduct_Test()
        {
            _mockSender = new Mock<ISender>();
        }

        [Fact]
        public async Task GetProduct_ShouldReturnOk_WhenProductIsFound()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var discountExpected = _faker.Random.Int(0, 100);
            var priceExpected = _faker.Random.Decimal(0, 10000);
            var statusNameExpected = _faker.PickRandom("Active", "Inactive");
            var finalPriceExpected = priceExpected - (priceExpected * discountExpected / 100);

            var expectedResponse = new ProductResponse(
                productId,
                _faker.Commerce.ProductName(),
                statusNameExpected,
                _faker.Random.Int(1, 1000), 
                _faker.Commerce.ProductDescription(),
                priceExpected,
                discountExpected,
                finalPriceExpected,
                DateTime.UtcNow,
                DateTime.UtcNow
            ); 

            _mockSender.Setup(x => x.Send(It.IsAny<GetProductQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(expectedResponse));

            var controller = new ProductsController(_mockSender.Object);

            // Act
            var result = await controller.GetProduct(productId, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = (OkObjectResult)result;
        }
      
    }
}