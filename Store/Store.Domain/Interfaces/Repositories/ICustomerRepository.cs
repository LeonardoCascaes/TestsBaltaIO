using Store.Domain.Entities;

namespace Store.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Customer Get(string document);
    }
}
