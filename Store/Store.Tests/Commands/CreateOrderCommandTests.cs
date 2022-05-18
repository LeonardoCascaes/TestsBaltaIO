using Store.Domain.Commands;
using System;
using Xunit;

namespace Store.Tests.Commands
{
    public class CreateOrderCommandTests
    {
        [Fact]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            var command = new CreateOrderCommand
            {
                Customer = "",
                ZipCode = "13411588",
                PromoCode = "12345678"
            };

            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.False(command.Valid);
        }
    }
}
