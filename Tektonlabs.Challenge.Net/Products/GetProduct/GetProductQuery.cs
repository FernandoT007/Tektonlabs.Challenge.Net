using Tektonlabs.Challenge.Net.Application.Abstractions.Messaging;

namespace Tektonlabs.Challenge.Net.Application.Products.GetProduct;

public sealed record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>;
