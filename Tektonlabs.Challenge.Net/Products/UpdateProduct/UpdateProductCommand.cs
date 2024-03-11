using Tektonlabs.Challenge.Net.Application.Abstractions.Messaging;
using Tektonlabs.Challenge.Net.Domain.Products;

namespace Tektonlabs.Challenge.Net.Application.Products.UpdateProduct;

public record UpdateProductCommand(
     Guid ProductId
    , string Name
    , ProductStatus Status
    , int Stock
    , string Description
    , decimal Price
    //, decimal Discount
    ) : ICommand<Guid>;