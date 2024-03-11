namespace Tektonlabs.Challenge.Net.Domain.Abstractions;
public interface IUnitOfWork
{
    Task<int> SaveChangeAsnyc(CancellationToken cancellationToken = default);
}
