using Tektonlabs.Challenge.Net.Application.Abstractions.Clock;
using Tektonlabs.Challenge.Net.Application.Abstractions.Messaging;
using Tektonlabs.Challenge.Net.Domain.Abstractions;
using Tektonlabs.Challenge.Net.Domain.Products;

namespace Tektonlabs.Challenge.Net.Application.Products.CreateProduct;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly PriceService _priceService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, PriceService priceService, IDateTimeProvider dateTimeProvider)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    //    _priceService = priceService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        
        var product = Product.Create(
                    Name.Create(request.Name).Value,               
                    Stock.Create(request.Stock).Value,
                    Description.Create(request.Description).Value,
                    Price.Create(request.Price).Value,
                    //Domain.Products.Discount.Create(request.Discount).Value,
                    _dateTimeProvider.currenTime
                    //_priceService
                    ).Value;

        _productRepository.Add(product);
        await _unitOfWork.SaveChangeAsnyc(cancellationToken);

        return product.Id;          
    }
}
