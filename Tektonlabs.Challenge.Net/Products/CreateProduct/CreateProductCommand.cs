using Tektonlabs.Challenge.Net.Application.Abstractions.Messaging;

namespace Tektonlabs.Challenge.Net.Application.Products.CreateProduct;

public record CreateProductCommand(
    string Name
    ,int Stock
    ,string Description
    ,decimal Price
    //, decimal Discount
    ): ICommand<Guid>;