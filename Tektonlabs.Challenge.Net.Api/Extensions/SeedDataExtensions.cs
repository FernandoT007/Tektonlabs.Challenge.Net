using Bogus;
using Dapper;
using Tektonlabs.Challenge.Net.Application.Abstractions.Data;
using Tektonlabs.Challenge.Net.Domain.Products;

namespace Tektonlabs.Challenge.Net.Api.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();
        List<object> productsFaker = new();
        for (var i = 0; i < 50; i++)
        {
            var priceFake = Convert.ToDecimal(faker.Commerce.Price());
            var discountFake = faker.Commerce.Random.Int(min: 0, max: 100);
            var dateCreateFake = faker.Date.Past(50);

            productsFaker.Add(new
            {
                Id = Guid.NewGuid(),
                Name = faker.Commerce.ProductName(),
                Status = faker.PickRandomWithout<ProductStatus>(),
                Stock = faker.Random.Int(min:0),
                Description = faker.Commerce.ProductDescription(),
                Price = priceFake,
                Discount = discountFake,
                FinalPrice = (priceFake * (100 - discountFake) / 100),
                CreateDate = dateCreateFake,
                LastUpdateDate = faker.Date.Between(dateCreateFake, DateTime.Now)
            }); 
        }

        const string sqlProduct = """
            INSERT INTO public.products
                (id, name, status, stock, description, price, discount, final_price, create_date, last_update_date)
                values(@Id, @Name,@Status,@Stock,@Description, @Price, @Discount,@FinalPrice, @CreateDate, @LastUpdateDate)
        """;

        connection.Execute(sqlProduct, productsFaker);

        List<object> status =
        [
            new
            {
                Id = Guid.NewGuid(),
                Key = 1,
                Value = "Active"
            },
            new
            {
                Id = Guid.NewGuid(),
                Key = 0,
                Value = "Inactive"
            },
        ];

        const string sqlStatus = """
            INSERT INTO public.status
                (id, key, value)
                values(@Id, @Key,@Value)
        """;

        connection.Execute(sqlStatus, status);
    }
}
