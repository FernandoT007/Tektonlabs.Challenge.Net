using System.Runtime.CompilerServices;

namespace Tektonlabs.Challenge.Net.Domain.Abstractions;

public record Error(string code, string message)
{
    public static Error None = new(string.Empty,string.Empty);
    public static Error NullValue = new("Error.NullValue", "Un valor null fue ingresado");
    public static Error Empty = new("Error.Empty", "Un valor vacio fue ingresado");

    
}

