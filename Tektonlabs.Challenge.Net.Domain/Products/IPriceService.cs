namespace Tektonlabs.Challenge.Net.Domain.Products;

public interface IPriceService
{
    public Price CalculatePrice(Price price, Discount discount);
}