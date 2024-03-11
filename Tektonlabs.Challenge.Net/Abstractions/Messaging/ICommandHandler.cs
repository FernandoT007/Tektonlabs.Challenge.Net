using MediatR;
using Tektonlabs.Challenge.Net.Application.Products.UpdateProduct;
using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Application.Abstractions.Messaging;


public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
where TCommand : ICommand
{
    Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand, TResponse> 
: IRequestHandler<TCommand, Result<TResponse>>
where TCommand : ICommand<TResponse>
{
    
}