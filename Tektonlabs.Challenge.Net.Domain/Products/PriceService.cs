namespace Tektonlabs.Challenge.Net.Domain.Products;

public class PriceService: IPriceService
{
    public Price CalculatePrice(Price price, Discount discount)
    {   
        return Price.Create(price.Value * (100 - discount.Value) / 100).Value;
    }
}
