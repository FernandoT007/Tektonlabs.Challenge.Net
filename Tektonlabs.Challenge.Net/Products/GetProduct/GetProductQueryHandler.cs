using Tektonlabs.Challenge.Net.Application.Abstractions.Messaging;
using Tektonlabs.Challenge.Net.Application.Discount;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;
using Tektonlabs.Challenge.Net.Domain.Status;

namespace Tektonlabs.Challenge.Net.Application.Products.GetProduct;

public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IDiscountService _discountService;

    public GetProductQueryHandler(IProductRepository productRepository, IStatusRepository statusRepository, IDiscountService discountService)
    {
        _productRepository = productRepository;
        _statusRepository = statusRepository;
        _discountService = discountService;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.NotFound);
        }
       
        var statusObj = await _statusRepository.GetByKeyAsync((int)product.Status,cancellationToken);
        if (statusObj == null)
        {
            return Result.Failure<ProductResponse>(Error.Empty);
        }

        var discount = _discountService.GetDiscountByProductId(request.ProductId).Result;
        var finalPrice = product.Price.Value - (product.Price.Value * discount / 100);

        return Result.Success(new ProductResponse(
            product.Id,
            product.Name.Value,
            statusObj.Value,
            product.Stock.Value,
            product.Description.Value,
            product.Price.Value,
            discount,
            finalPrice,
            product.CreateDate,
            product.LastUpdateDate
            ));
    }

}
