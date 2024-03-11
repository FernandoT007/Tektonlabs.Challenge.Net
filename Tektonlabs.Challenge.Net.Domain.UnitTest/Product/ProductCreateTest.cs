using FluentAssertions;
using System;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;
using Tektonlabs.Challenge.Net.Domain.UnitTest.Helper;

namespace Tektonlabs.Challenge.Net.Domain.UnitTest.Product;

public class ProductCreateTest : ProductTestBase
{
    [Fact]
    public void CreateProduct_ShouldReturnSuccess()
    {
        // Arrange
        var price = Price.Create(100).Value;
        //var discount = Discount.Create(10).Value;
        //var finalPrice = ProductTestHelper.CalculateFinalPrice(price, discount);
        var expectedName = Name.Create(_faker.Commerce.ProductName()).Value;
        var expectedDescription = Description.Create(_faker.Lorem.Sentence()).Value;
        var expectedStatus = ProductStatus.Active;
        var expectedStock = Stock.Create(_faker.Random.Int(1, 1000)).Value;

        //ProductTestHelper.SetupPriceServiceMock(_mockPriceService, price, discount, finalPrice);

        // Act
        var result = Products.Product.Create(
            expectedName,
            expectedStock,
            expectedDescription,
            price,
            // discount,
            DateTime.UtcNow
            //_mockPriceService.Object
        );

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Result<Products.Product>>();    

        var createdProduct = result.Value;

        ProductTestHelper.AssertProductProperties(
            createdProduct,
            expectedName,
            expectedDescription,
            expectedStatus,
            expectedStock,
            price,
            //discount,
            //finalPrice,
            createdProduct.CreateDate,
            createdProduct.LastUpdateDate
        );
    }

    [Fact]
    public void CreateProduct_WithNullName_ShouldReturnError()
    {
        // Arrange
        var price = Price.Create(100).Value;
        //var discount = Discount.Create(10).Value;

        // Act
        var result = Products.Product.Create(
            null,
            Stock.Create(10).Value,
            Description.Create(_faker.Lorem.Sentence()).Value,
            price,
            //discount,
            DateTime.UtcNow
           // _mockPriceService.Object
        );

        // Assert
        result.Should().BeOfType<Result<Products.Product>>();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(Error.NullValue);
    }

    [Fact]
    public void CreateProduct_WithInvalidPrice_ShouldReturnError()
    {
        // Arrange

        // Act
        var result = Products.Product.Create(
            Name.Create(_faker.Commerce.ProductName()).Value,      
            Stock.Create(10).Value,
            Description.Create(_faker.Lorem.Sentence()).Value,
            null,
            //Discount.Create(10).Value,
            DateTime.UtcNow
           // _mockPriceService.Object
        );

        // Assert
        result.Should().BeOfType<Result<Products.Product>>();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(Error.NullValue);
    }

    //[Fact]
    //public void CreateProduct_WithInvalidDiscount_ShouldReturnError()
    //{
    //    // Arrange

    //    // Act
    //    var result = Products.Product.Create(
    //        Name.Create(_faker.Commerce.ProductName()).Value,
    //        ProductStatus.Active,
    //        Stock.Create(10).Value,
    //        Description.Create(_faker.Lorem.Sentence()).Value,
    //        Price.Create(100).Value,
    //        // Cambio: Se crea un descuento inválido
    //        null,
    //        DateTime.UtcNow,
    //        _mockPriceService.Object
    //    );

    //    // Assert
    //    result.Should().BeOfType<Result<Products.Product>>();
    //    result.IsSuccess.Should().BeFalse();
    //    result.Error.Should().Be(Error.NullValue);
    //}
}