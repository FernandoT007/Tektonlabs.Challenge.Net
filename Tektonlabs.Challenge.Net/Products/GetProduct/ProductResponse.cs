namespace Tektonlabs.Challenge.Net.Application.Products.GetProduct;

public sealed class ProductResponse
{
    public ProductResponse(Guid id, string name, string statusName, int stock, string description, decimal price, int discount, decimal finalPrice, DateTime createDate, DateTime lastUpdateDate)
    {
        ProductId = id;
        Name = name;
        StatusName = statusName;
        Stock = stock;
        Description = description;
        Price = price;
        Discount = discount;
        FinalPrice = finalPrice;
        CreateDate = createDate;
        LastUpdateDate = lastUpdateDate;
    }

    public Guid ProductId { get; init; }
    public string Name { get; init; }
    public string StatusName { get; init; }
    public int Stock { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public int Discount { get; init; }
    public decimal FinalPrice { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime LastUpdateDate { get; init; }
}
