using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Products;

public sealed record Discount
{
    public static readonly Error Invalid = new("Discount.Invalid", "El porcentaje de descuento es invalido");

    public decimal Value { get; init; }
    private Discount(decimal _value) => Value = _value;

    public static Result<Discount> Create(decimal value)
    {
        if (value < 0 || value > 100  )
        {
            return Result.Failure<Discount>(Invalid);
        }
        return new Discount(value);
    }


}
