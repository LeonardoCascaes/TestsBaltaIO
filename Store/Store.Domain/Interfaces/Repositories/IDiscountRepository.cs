using Store.Domain.Entities;

namespace Store.Domain.Interfaces.Repositories
{
    public interface IDiscountRepository
    {
        Discount Get(string code);
    }
}
