using Store.Domain.Entities;

namespace Store.Domain.Repositories.Interfaces
{
  public interface IProductRepository
  {
    IEnumerable<Product> Get(IEnumerable<Guid> ids);
    IEnumerable<Product> GetAll();
  }
}
