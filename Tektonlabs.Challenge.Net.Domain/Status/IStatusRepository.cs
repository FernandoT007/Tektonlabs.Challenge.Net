namespace Tektonlabs.Challenge.Net.Domain.Status;

public interface IStatusRepository
{
    Task<Status?> GetByKeyAsync(int key, CancellationToken cancellationToken = default);
}
