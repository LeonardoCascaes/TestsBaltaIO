namespace Store.Domain.Entities
{
    public class BaseEntity
    {
        public Guid Guid { get; private set; }

        public BaseEntity()
        {
            Guid = Guid.NewGuid();
        }
    }
}
