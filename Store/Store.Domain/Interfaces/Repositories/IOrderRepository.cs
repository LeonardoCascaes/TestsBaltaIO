using Store.Domain.Entities;

namespace Store.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
