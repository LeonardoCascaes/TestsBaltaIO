using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Interfaces.Repositories;
using Store.Domain.Utils;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountRepository _discountRepository;

        public OrderHandler(
            ICustomerRepository customerRepository, IOrderRepository orderRepository, 
            IDeliveryFeeRepository deliveryFeeRepository, IProductRepository productRepository, 
            IDiscountRepository discountRepository
            )
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _productRepository = productRepository;
            _discountRepository = discountRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

            var customer = _customerRepository.Get(command.Customer);
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);
            var discount = _discountRepository.Get(command.PromoCode);
            var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();

            var order = new Order(customer, deliveryFee, discount);

            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Id).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            AddNotifications(order.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar pedido", Notifications);

            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);

        }
    }
}
