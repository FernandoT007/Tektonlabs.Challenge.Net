using Tektonlabs.Challenge.Net.Application.Abstractions.Clock;
using Tektonlabs.Challenge.Net.Application.Abstractions.Messaging;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;

namespace Tektonlabs.Challenge.Net.Application.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly PriceService _priceService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider) // , PriceService priceService
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
       //_priceService = priceService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productToUpdate = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (productToUpdate == null)
        {
            return Result.Failure<Guid>(ProductErrors.NotFound);
        }

            productToUpdate.Update(
            Name.Create(request.Name).Value,
            request.Status,
            Stock.Create(request.Stock).Value,
            Description.Create(request.Description).Value,
            Price.Create(request.Price).Value,
            //Domain.Products.Discount.Create(request.Discount).Value,
            _dateTimeProvider.currenTime
            //_priceService
            );

        _productRepository.Update(productToUpdate);
        await _unitOfWork.SaveChangeAsnyc(cancellationToken);

        return productToUpdate.Id;

    }
}
