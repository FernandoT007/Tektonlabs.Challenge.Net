using Tektonlabs.Challenge.Net.Domain.Abstractions;

namespace Tektonlabs.Challenge.Net.Domain.Products;

public static class ProductErrors
{
    public static Error NotFound = new(
        "Products.NotFound",
        "No existe un producto con este id"
    );

    public static Error FailedUpdate = new(
      "Products.FailedUpdate",
      "Ocurrio un error al actualizar el producto"
    );

    public static Error FailedCreation = new(
     "Products.FailedCreation",
     "Ocurrio un error al crear el producto"
    );
}
