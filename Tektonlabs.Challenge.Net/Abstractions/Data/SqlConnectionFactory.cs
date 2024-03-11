using System.Data;

namespace Tektonlabs.Challenge.Net.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
