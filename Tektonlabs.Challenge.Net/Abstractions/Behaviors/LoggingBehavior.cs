using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Tektonlabs.Challenge.Net.Application.Abstractions.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> _logger;
    private readonly string _logFilePath;

    public LoggingBehavior(ILogger<TRequest> logger, IConfiguration configuration)
    {
        _logger = logger;
        _logFilePath = configuration["Logging:FilePath"] ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;
        var stopwatch = new Stopwatch();
        try
        {
            _logger.LogInformation($"Ejecutando el command request: {name}");
            stopwatch.Start();
            var result = await next();
            stopwatch.Stop();
            _logger.LogInformation($"El comando {name} se ejecuto exitosamente");
            await LogRequestDurationAsync(name, stopwatch.ElapsedMilliseconds);
            return result;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, $"El comando {name} tuvo errores");
            throw;
        }
    }

    private async Task LogRequestDurationAsync(string requestName, long durationMs)
    {
        using (var writer = new StreamWriter(_logFilePath, true))
        {
            await writer.WriteLineAsync($"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {requestName}: {durationMs} ms");
        }
    }
}