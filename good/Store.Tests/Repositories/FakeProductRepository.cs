using Store.Domain.Entities;
using Store.Domain.Queries;
using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
  internal class FakeProductRepository : IProductRepository
  {
    private readonly IList<Product> _products;

    public FakeProductRepository()
    {
      _products = new List<Product>
      {
        new Product("Produto 01", 10, true),
        new Product("Produto 02", 10, true),
        new Product("Produto 03", 10, true),
        new Product("Produto 04", 10, false),
        new Product("Produto 05", 10, false)
      };
    }

    public IEnumerable<Product> Get(IEnumerable<Guid> ids)
    {
      IList<Product> products = new List<Product>();

      foreach (var id in ids) 
      { 
        var product = _products.FirstOrDefault(x => x.Id == id);
        if (product is not null)
          products.Add(product);
      }

      return products;
    }

    public IEnumerable<Product> GetAll() => _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
  }
}
