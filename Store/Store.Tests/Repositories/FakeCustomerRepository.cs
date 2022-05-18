using Store.Domain.Entities;
using Store.Domain.Interfaces.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "12345678911")
                return new Customer("Leonardo", "Leo@hotmail.com");

            return null;
        }
    }
}
