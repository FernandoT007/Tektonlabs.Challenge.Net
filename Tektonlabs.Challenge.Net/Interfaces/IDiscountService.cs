namespace Tektonlabs.Challenge.Net.Application.Discount;


public interface IDiscountService
{
    Task<int> GetDiscountByProductId(Guid productId);
}

