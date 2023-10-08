using Store.Domain.Repositories.Interfaces;

namespace Store.Tests.Repositories
{
  internal class FakeDeliveryFeeRepository : IDeliveryFeeRepository
  {
    public decimal Get(string zipCode)
    {
      return 10;
    }
  }
}
