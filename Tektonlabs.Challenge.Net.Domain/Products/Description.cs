using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Products;

public sealed record Description 
{
    public static readonly Error Empty = new("Description.Empty", "La descripción no puede ser vacia");
    public string Value { get; init; }

    private Description(string _value) => Value = _value;

    public static Result<Description> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Description>(Empty);
        }
        return new Description(value);
    }

}