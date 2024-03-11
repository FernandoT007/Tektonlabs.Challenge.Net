using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Products;
public sealed class Product : Entity
{
    private Product(
        Guid id
        , Name name
        , ProductStatus status
        , Stock stock
        , Description description
        , Price price)
     //   , Discount discount
    //    , Price finalPrice)
        : base(id)
    {
        Name = name;
        Status = status;
        Stock = stock;
        Description = description;
        Price = price;
     //   Discount = discount;
     //   FinalPrice = finalPrice; 
    }

    private Product() { }
    public Name Name { get; private set; }
    public ProductStatus Status { get; private set; }
    public Stock Stock { get; private set; }
    public Description Description { get; private set; }
    public Price Price { get; private set; }
  //  public Discount Discount { get; internal set; }
  //  public Price FinalPrice { get; private set; }
    public DateTime CreateDate { get; internal set; }
    public DateTime LastUpdateDate { get; internal set; }

    public static Result<Product> Create(
           Name name,       
           Stock stock,
           Description description,
           Price price,
        // Discount discount,
           DateTime createTime  
        // IPriceService priceService
        )
    {

        if (name == null || stock == null || description == null || price == null) // || discount == null || priceService == null)
        {
            return Result.Failure<Product>(Error.NullValue);
        }    

      //  var finalPrice = priceService.CalculatePrice(price, discount);

        var product = new Product(
            Guid.NewGuid(),
            name,
            ProductStatus.Active,
            stock,  
            description,
            price 
            //,discount
            //,finalPrice
            );

        product.LastUpdateDate = createTime;
        product.CreateDate = createTime;

        return product;
    }

    public Result Update(
           Name name,
           ProductStatus status,
           Stock stock,
           Description description,
           Price price,
        //   Discount discount,
           DateTime lastUpdateTime
        //   IPriceService priceService
        )
    {

    //    var finalPrice = priceService.CalculatePrice(price, discount);

        Name = name;
        Status = status;
        Stock = stock;
        Description = description;
        Price = price;
       // Discount = discount;
       // FinalPrice = finalPrice;
        LastUpdateDate = lastUpdateTime;
        
        return Result.Success();
    }
}
