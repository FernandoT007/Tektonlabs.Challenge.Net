using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tektonlabs.Challenge.Net.Application.Products.CreateProduct;
using Tektonlabs.Challenge.Net.Application.Products.GetProduct;
using Tektonlabs.Challenge.Net.Application.Products.UpdateProduct;

namespace Tektonlabs.Challenge.Net.Api.Controllers.Product
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISender sender;

        public ProductsController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProductQuery(id);
            var resultado = await sender.Send(query, cancellationToken);
            return resultado.IsSuccess ? Ok(resultado) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command, CancellationToken cancellationToken)
        {       
            var resultado = await sender.Send(command, cancellationToken);
            if (resultado.IsFailure)
            {
                return BadRequest(resultado.Error);
            }
           return CreatedAtAction(nameof(this.GetProduct), new { id = resultado.Value }, resultado.Value);        
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct( UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var resultado = await sender.Send(command, cancellationToken);
            if (resultado.IsFailure)
            {
                return BadRequest(resultado.Error);
            }
            return CreatedAtAction(nameof(GetProduct), new { id = resultado.Value }, resultado.Value);
        }
    }
}
