using Tektonlabs.Challenge.Net.Domain.Products;

namespace Tektonlabs.Challenge.Net.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {

    }
}
