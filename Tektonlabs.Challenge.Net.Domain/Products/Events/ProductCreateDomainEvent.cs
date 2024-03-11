using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Products.Events;

public sealed record ProductCreateDomainEvent(Guid Guid) : IDomainEvent;