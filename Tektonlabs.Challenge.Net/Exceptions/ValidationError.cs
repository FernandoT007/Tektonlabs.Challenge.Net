namespace Tektonlabs.Challenge.Net.Application.Exceptions;

public sealed record ValidationError(
    string PropertyName,
    string ErrorName
    );