using Store.Domain.Entities;

namespace Store.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get(IEnumerable<Guid> ids);
    }
}
