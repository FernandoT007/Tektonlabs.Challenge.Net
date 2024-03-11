using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tektonlabs.Challenge.Net.Api.Controllers.Product;
using Tektonlabs.Challenge.Net.Application.Products.CreateProduct;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Xunit;

namespace Tektonlabs.Challenge.Net.Api.Test.Controllers.Product
{
    public class ProductsController_CreateProduct_Test
    {
        private readonly Mock<ISender> _mockSender;
        private readonly Faker _faker = new();

        public ProductsController_CreateProduct_Test()
        {
            _mockSender = new Mock<ISender>();
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnCreated_WhenProductIsCreatedSuccessfully()
        {
            // Arrange
            var id = Guid.NewGuid();

            var createProductCommand = new CreateProductCommand(
                _faker.Commerce.ProductName(),
                _faker.Random.Int(1, 100),
                _faker.Commerce.ProductDescription(),
                _faker.Random.Decimal(1, 10000)
            );

            _mockSender.Setup(x => x.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(id));

            var controller = new ProductsController(_mockSender.Object);

            // Act
            var result = await controller.CreateProduct(createProductCommand, CancellationToken.None);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdResult = (CreatedAtActionResult)result;

            createdResult.ActionName.Should().BeEquivalentTo(nameof(controller.GetProduct));
            createdResult.Value.Should().BeEquivalentTo(id);
        }
    }
}