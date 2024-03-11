using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Products;

public sealed record Name
{
    public static readonly Error Empty = new("Name.Empty", "El nombre no puede ser vacio");
    public string Value { get; init; }

    private Name(string _value) => Value = _value;

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Name>(Empty);
        }
        return new Name(value);
    }

}