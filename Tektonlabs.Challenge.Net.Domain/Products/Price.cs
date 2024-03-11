using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Products;

public sealed record Price
{
    public static readonly Error Invalid = new("Price.Invalid", "El precio debe ser mayor a 0");

    public decimal Value { get; init; }
    private Price(decimal _value) => Value = _value;

    public static Result<Price> Create(decimal value)
    {
        if (value <= 0)
        {
            return Result.Failure<Price>(Invalid);
        }
        return new Price(value);
    }
}
