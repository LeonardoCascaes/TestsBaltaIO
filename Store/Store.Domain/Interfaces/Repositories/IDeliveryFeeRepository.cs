namespace Store.Domain.Interfaces.Repositories
{
    public interface IDeliveryFeeRepository
    {
        decimal Get(string zipCode);
    }
}
