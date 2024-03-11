using Bogus;
using Moq;
using Tektonlabs.Challenge.Net.Domain.Products;

namespace Tektonlabs.Challenge.Net.Domain.UnitTest.Helper;

public abstract class ProductTestBase
{
    protected readonly Mock<IPriceService> _mockPriceService = new();
    protected readonly Faker _faker = new();

    protected Products.Product CreateProduct(Price price) //, Discount discount, Price finalPrice)
    {
        // Configurar el mock de IPriceService
       // ProductTestHelper.SetupPriceServiceMock(_mockPriceService, price, discount, finalPrice);

        // Crear el producto usando el método Create
        var product = Products.Product.Create(
            Name.Create(_faker.Commerce.ProductName()).Value,
            Stock.Create(_faker.Random.Int(1, 1000)).Value,
            Description.Create(_faker.Lorem.Sentence()).Value,
            price,
          //  discount,
            DateTime.UtcNow
         //   _mockPriceService.Object
        ).Value;

        return product;
    }
}