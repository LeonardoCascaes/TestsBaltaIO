using Store.Domain.Entities;
using Store.Domain.Interfaces.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
        }
    }
}
