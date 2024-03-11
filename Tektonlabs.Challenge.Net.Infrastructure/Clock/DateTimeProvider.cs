using Tektonlabs.Challenge.Net.Application.Abstractions.Clock;

namespace Tektonlabs.Challenge.Net.Infrastructure.Clock;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime currenTime => DateTime.UtcNow;

}
