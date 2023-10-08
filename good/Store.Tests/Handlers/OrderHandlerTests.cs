using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
  [TestClass]
  public class OrderHandlerTests
  {
    private readonly ICustomerRepository _customerRepository;
    private readonly IDeliveryFeeRepository _deliveryFeeRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IEnumerable<CreateOrderItemCommand> _orderItems;
    private readonly OrderHandler _handler;

    public OrderHandlerTests()
    {
      _customerRepository = new FakeCustomerRepository();
      _deliveryFeeRepository = new FakeDeliveryFeeRepository();
      _discountRepository = new FakeDiscountRepository();
      _orderRepository = new FakeOrderRepository();
      _productRepository = new FakeProductRepository();
      _orderItems = _productRepository.GetAll()
        .Take(2)
        .Select(x => new CreateOrderItemCommand(x.Id, 1));

      _handler = new OrderHandler(
        _customerRepository, 
        _deliveryFeeRepository, 
        _discountRepository, 
        _productRepository, 
        _orderRepository
      );
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
    {
      var command = new CreateOrderCommand(
        customer: "62224038089", 
        zipCode: "11111111", 
        promoCode: "12345678", 
        _orderItems.ToList()
      );

      _handler.Handle(command);

      Assert.IsTrue(_handler.Invalid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_cep_invalido_o_pedido_nao_deve_ser_gerado()
    {
      var command = new CreateOrderCommand(
        customer: "12345678911",
        zipCode: "1111111",
        promoCode: "12345678",
        _orderItems.ToList()
      );

      _handler.Handle(command);

      Assert.IsTrue(_handler.Invalid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
    {
      var command = new CreateOrderCommand(
        customer: "12345678911",
        zipCode: "13411080",
        promoCode: "",
        _orderItems.ToList()
      );

      _handler.Handle(command);

      Assert.IsTrue(_handler.Valid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
    {
      var command = new CreateOrderCommand(
        customer: "12345678911",
        zipCode: "13411080",
        promoCode: "12345678",
        new List<CreateOrderItemCommand>()
      );

      _handler.Handle(command);

      Assert.IsTrue(_handler.Invalid);
    }

    [TestMethod]
    [TestCategory("Handlers")]
    public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
    {
      var command = new CreateOrderCommand(
        customer: "12345678911",
        zipCode: "13411080",
        promoCode: "12345678",
        _orderItems.ToList()
      );

      _handler.Handle(command);

      Assert.IsTrue(_handler.Valid);
    }
  }
}
