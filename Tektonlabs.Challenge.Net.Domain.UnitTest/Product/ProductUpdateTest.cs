using Tektonlabs.Challenge.Net.Domain.Products;
using Tektonlabs.Challenge.Net.Domain.UnitTest.Helper;

namespace Tektonlabs.Challenge.Net.Domain.UnitTest.Product;

public class ProductUpdateTest : ProductTestBase
{

    [Fact]
    public void Product_Update_UpdatesAllPropertiesCorrectly()
    {
        // Arrange
       // var (price, discount, finalPrice) = CreateRandomPriceDetails();
        var product = CreateProduct(Price.Create(_faker.Random.Decimal(0, 10000)).Value);
        var updatedName = Name.Create(_faker.Commerce.ProductName()).Value;
        var updatedDescription = Description.Create(_faker.Lorem.Sentence()).Value;
        var updatedStatus = _faker.PickRandom<ProductStatus>();
        var updatedStock = Stock.Create(_faker.Random.Int(1, 1000)).Value;
        var updatedPrice = Price.Create(_faker.Random.Decimal(0, 1000000)).Value;
        //var updatedDiscount = Discount.Create(_faker.Random.Decimal(0, 100)).Value;
        var updateDate = _faker.Date.Recent();
        //var expectedFinalPrice = ProductTestHelper.CalculateFinalPrice(updatedPrice, updatedDiscount);

        //ProductTestHelper.SetupPriceServiceMock(_mockPriceService, updatedPrice, updatedDiscount, expectedFinalPrice);

        // Act
        var result = product.Update(
            updatedName,
            updatedStatus,
            updatedStock,
            updatedDescription,
            updatedPrice,
           // updatedDiscount,
            updateDate
            //_mockPriceService.Object
        );

        // Assert
        ProductTestHelper.AssertUpdateResult(result, product, updatedName, updatedDescription, updatedStatus, updatedStock, updatedPrice, updateDate); // updatedDiscount, expectedFinalPrice);
    }

    //private (Price, Discount, Price) CreateRandomPriceDetails()
    //{
    //    var price = Price.Create(_faker.Random.Decimal(0, 1000000)).Value;
    //    var discount = Discount.Create(_faker.Random.Decimal(0, 100)).Value;
    //    var finalPrice = ProductTestHelper.CalculateFinalPrice(price, discount);

    //    return (price, discount, finalPrice);
    //}
}