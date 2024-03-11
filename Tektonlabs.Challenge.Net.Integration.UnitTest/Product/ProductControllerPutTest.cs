using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tektonlabs.Challenge.Net.Api.Controllers.Product;
using Tektonlabs.Challenge.Net.Application.Products.UpdateProduct;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;
using Xunit;

namespace Tektonlabs.Challenge.Net.Api.Test.Controllers.Product
{
    public class ProductsController_UpdateProduct_Test
    {
        private readonly Mock<ISender> _mockSender;
        private readonly Faker _faker = new();

        public ProductsController_UpdateProduct_Test()
        {
            _mockSender = new Mock<ISender>();
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnOk_WhenProductIsUpdatedSuccessfully()
        {
            // Arrange
            var existingProductId = Guid.NewGuid();
        
            var updateProductCommand = new UpdateProductCommand(
                existingProductId,
                _faker.Commerce.ProductName(),
                _faker.PickRandom<ProductStatus>(),
                _faker.Random.Int(1, 100), 
                _faker.Commerce.ProductDescription(),
                _faker.Random.Decimal(1, 10000)
            );

            _mockSender.Setup(x => x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(existingProductId));

            var controller = new ProductsController(_mockSender.Object);

            // Act
            var result = await controller.UpdateProduct(updateProductCommand, CancellationToken.None);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = (CreatedAtActionResult)result;

            createdResult.ActionName.Should().BeEquivalentTo(nameof(controller.GetProduct));
            createdResult.Value.Should().BeEquivalentTo(existingProductId);
        }
    }
}