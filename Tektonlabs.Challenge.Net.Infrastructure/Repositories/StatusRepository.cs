using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Tektonlabs.Challenge.Net.Domain.Status;

namespace Tektonlabs.Challenge.Net.Infrastructure.Repositories;

internal sealed class StatusRepository : Repository<Status>, IStatusRepository
{
    private readonly IMemoryCache _memoryCache;

    public StatusRepository(ApplicationDbContext dbContext, IMemoryCache memoryCache) : base(dbContext)
    { 
        _memoryCache = memoryCache;
    }

    public Task<Status?> GetByKeyAsync(int key, CancellationToken cancellationToken = default)
    {
        string keyCached = $"status-{key}";
        return _memoryCache.GetOrCreateAsync(
            keyCached,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                return GetByKeyFromDataSourceAsync(key, cancellationToken);
            });
    }

    private async Task<Status?> GetByKeyFromDataSourceAsync(int key, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Status>()
        .FirstOrDefaultAsync(t => t.Key == key, cancellationToken);
    }
}
