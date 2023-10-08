using Store.Domain.Entities;
using System.Linq.Expressions;

namespace Store.Domain.Queries
{
  public class ProductQueries
  {
    public static Expression<Func<Product, bool>> GetActiveProducts() => x => x.Active;

    public static Expression<Func<Product, bool>> GetInactiveProducts() => x => x.Active == false;
  }
}
