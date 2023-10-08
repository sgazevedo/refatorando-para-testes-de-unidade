using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;

namespace Store.Tests.Commands
{
  [TestClass]
  public class CreateOrderCommandTests
  {
    [TestMethod]
    [TestCategory("Commands")]
    public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
    {
      var orderItems = new List<CreateOrderItemCommand>
      {
        new CreateOrderItemCommand { Product = Guid.NewGuid(), Quantity = 1 },
        new CreateOrderItemCommand { Product = Guid.NewGuid(), Quantity = 1 },
      };

      var command = new CreateOrderCommand(customer: "", zipCode: "11111111", promoCode: "1111111", items: orderItems);

      command.Validate();

      Assert.IsTrue(command.Invalid);
    }
  }
}
