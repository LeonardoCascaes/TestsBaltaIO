using Flunt.Notifications;

namespace Store.Domain.Entities
{
    public class BaseEntity : Notifiable
    {
        public Guid Id { get; private set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
