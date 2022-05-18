using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Interfaces.Repositories;
using Store.Tests.Repositories;
using System;
using Xunit;

namespace Store.Tests.Handlers
{
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly OrderHandler _handler;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _orderRepository = new FakeOrderRepository(); 
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _productRepository = new FakeProductRepository();
            _discountRepository = new FakeDiscountRepository();
            _handler = new OrderHandler(_customerRepository, _orderRepository, _deliveryFeeRepository, _productRepository, _discountRepository);
        }

        [Fact]
        public void DadoUmClienteInexistenteOPedidoNaoDeveSerGerado()
        {
            var command = CriarOrderCommandValido();
            command.Customer = "11111111111";

            _handler.Handle(command);
            Assert.False(_handler.Valid);
        }

        [Fact]
        public void DadoUmCepInvalidoOPedidoDeveSerGeradoNormalmente()
        {
            var command = CriarOrderCommandValido();
            command.ZipCode = "10101010";

            _handler.Handle(command);
            Assert.True(_handler.Valid);
        }

        [Fact]
        public void DadoPromoCodeInexistenteOPedidoDeveSerGeradoNormalmente()
        {
            var command = CriarOrderCommandValido();
            command.PromoCode = "11111111";

            _handler.Handle(command);
            Assert.True(_handler.Valid);
        }

        [Fact]
        public void DadoUmComandoInvalidoOPedidoNaoDeveSerGerado()
        {
            var command = CriarOrderCommandValido();
            command.Customer = "";
            command.Validate();

            Assert.False(command.Valid);
        }

        [Fact]
        public void DadoUmComandoValidoOPedidoDeveSerGerado()
        {
            var command = CriarOrderCommandValido();

            _handler.Handle(command);
            Assert.True(_handler.Valid);
        }
        private static CreateOrderCommand CriarOrderCommandValido()
        {
            var command = new CreateOrderCommand()
            {
                Customer = "12345678911",
                ZipCode = "12345688",
                PromoCode = "12345678"
            };

            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            return command;
        }
    }
}
