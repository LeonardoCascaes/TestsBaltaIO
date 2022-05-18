using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class CreateOrderItemCommand : Notifiable, ICommand
    {
        public CreateOrderItemCommand()
        {
        }

        public CreateOrderItemCommand(Guid id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public Guid Id { get; set; }
        public int Quantity { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasLen(Id.ToString(), 32, "Id", "Id inválido")
                .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade inválida")
                );
        }
    }
}
