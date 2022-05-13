namespace Store.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(string tittle, decimal price, bool active)
        {
            Tittle = tittle;
            Price = price;
            Active = active;
        }

        public string Tittle { get; private set; }
        public decimal Price { get; private set; }
        public bool Active { get; private set; }
    }
}
