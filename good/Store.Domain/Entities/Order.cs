using Store.Domain.Enums;

namespace Store.Domain.Entities
{
  public class Order : Entity
  {
    public Order(Customer customer, decimal deliveryFee, Discount discount) : base()
    {
      Customer = customer;
      Date = DateTime.Now;
      Number = Guid.NewGuid().ToString()[..8];
      Status = OrderStatus.WaitingPayment;
      DeliveryFee = deliveryFee;
      Discount = discount;
      Items = new List<OrderItem>();
    }

    public Customer Customer { get; private set; }
    public DateTime Date { get; private set; }
    public string Number { get; private set; }
    public IList<OrderItem> Items { get; private set; }
    public decimal DeliveryFee { get; private set; }
    public Discount Discount { get; private set; }
    public OrderStatus Status { get; private set; }

    public void AddItem(Product product, int quantity)
    {
      var item = new OrderItem(product, quantity);
      Items.Add(item);
    }

    public decimal Total()
    {
      decimal total = 0;
      foreach (var item in Items)
      {
        total += item.Total();
      }

      total += DeliveryFee;
      total -= Discount != null ? Discount.Value() : 0;

      return total;
    }

    public void Pay(decimal amount)
    {
      if (amount == Total())
        Status = OrderStatus.WaitingDelivery;
    }

    public void Cancel()
    {
      Status = OrderStatus.Canceled;
    }
  }
}
