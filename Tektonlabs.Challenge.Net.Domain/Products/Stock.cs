using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Products;

public sealed record Stock
{
    public static readonly Error Invalid = new("Stock.Invalid", "El stock no puede ser menor a 0");
    public int Value { get; init; }
    private Stock(int _value) => Value = _value;

    public static Result<Stock> Create(int value)
    {
        if (value <= 0)
        {
            return Result.Failure<Stock>(Invalid);
        }
        return new Stock(value);
    }
}