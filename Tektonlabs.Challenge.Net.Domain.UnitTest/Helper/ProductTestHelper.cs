using FluentAssertions;
using Moq;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;

public static class ProductTestHelper
{
    //public static void SetupPriceServiceMock(Mock<IPriceService> priceService, Price price, Discount discount, Price finalPrice)
    //{
    //    priceService.Setup(x => x.CalculatePrice(It.IsAny<Price>(), It.IsAny<Discount>()))
    //        .Returns(finalPrice);
    //}

    //public static Price CalculateFinalPrice(Price price, Discount discount)
    //{
    //    return Price.Create(price.Value * (100 - discount.Value) / 100).Value;
    //}

    public static void AssertProductProperties(
      Product product,
      Name name,
      Description description,
      ProductStatus status,
      Stock stock,
      Price price,
      //Discount discount,
      //Price expectedFinalPrice,
      DateTime expectedCreateDate,
      DateTime expectedLastUpdateDate
  )
    {
        product.Name.Should().Be(name);
        product.Description.Should().Be(description);
        product.Status.Should().Be(status);
        product.Stock.Should().Be(stock);
        product.Price.Should().Be(price);
        //product.Discount.Should().Be(discount);
        //product.FinalPrice.Should().Be(expectedFinalPrice);
        product.CreateDate.Should().Be(expectedCreateDate);
        product.LastUpdateDate.Should().Be(expectedLastUpdateDate);
    }

    public static void AssertUpdateResult(
        Result result,
        Product product,
        Name expectedName,
        Description expectedDescription,
        ProductStatus expectedStatus,
        Stock expectedStock,
        Price expectedPrice,
        //Discount expectedDiscount,
        //Price expectedFinalPrice,
        DateTime expectedLastUpdateDate
    )
    {
        if (!result.IsSuccess)
        {       
            throw new InvalidOperationException("El resultado no es el esperado");
        }

        var updatedProduct = product;

        AssertProductProperties(
            updatedProduct,
            expectedName,
            expectedDescription,
            expectedStatus,
            expectedStock,
            expectedPrice,
            //expectedDiscount,
            //expectedFinalPrice,
            product.CreateDate,
            expectedLastUpdateDate
        );
    }
}