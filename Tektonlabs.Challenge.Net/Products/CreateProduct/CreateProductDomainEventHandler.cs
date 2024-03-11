using MediatR;
using Tektonlabs.Challenge.Net.Domain.Products.Events;

namespace Tektonlabs.Challenge.Net.Application.Products.CreateProduct;

internal sealed class CreateProductDomainEventHandler : INotificationHandler<ProductCreateDomainEvent>
{
    public Task Handle(ProductCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
