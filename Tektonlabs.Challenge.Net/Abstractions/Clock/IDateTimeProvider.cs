namespace Tektonlabs.Challenge.Net.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime currenTime { get; }
}
