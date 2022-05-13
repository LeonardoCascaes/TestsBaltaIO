using Flunt.Notifications;

namespace Store.Domain.Entities
{
    public class BaseEntity : Notifiable
    {
        public Guid Guid { get; private set; }

        public BaseEntity()
        {
            Guid = Guid.NewGuid();
        }
    }
}
