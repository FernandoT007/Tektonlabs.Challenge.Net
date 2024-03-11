using System.Text.Json;
using Tektonlabs.Challenge.Net.Application.Discount;

namespace Tektonlabs.Challenge.Net.Domain.Discount;

public class DiscountService : IDiscountService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DiscountService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<int> GetDiscountByProductId(Guid productId)
    {     
        using var httpClient = _httpClientFactory.CreateClient("DiscountHttpClient"); 

        var response = await httpClient.GetAsync("api/Discount");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Error obtener los descuentos del servicio externo.");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var discountResponse = JsonSerializer.Deserialize<IEnumerable<DiscountResponse>>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });

        if (discountResponse == null || !discountResponse.Any())
        {
            return 0;
        }
        else
        {
            var discountResult = discountResponse.FirstOrDefault(x => x.IdProduct == productId);
            return discountResult == null ? 0 : int.Parse(discountResult.Value);
        }
    }

    public record DiscountResponse(Guid IdProduct, string Value);
   
}
