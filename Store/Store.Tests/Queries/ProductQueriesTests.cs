using Store.Domain.Entities;
using Store.Domain.Queries;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Store.Tests.Queries
{
    public class ProductQueriesTests
    {
        private readonly List<Product> _products;

        public ProductQueriesTests()
        {
            _products = new List<Product>
            {
                new Product("Produto 01", 10, true),
                new Product("Produto 02", 20, true),
                new Product("Produto 03", 30, true),
                new Product("Produto 04", 40, false),
                new Product("Produto 05", 50, false)
            };
        }

        [Fact]
        public void DadoAConsultaDeProdutosAtivosDeveRetornar3()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void DadoAConsultaDeProdutosInativosDeveRetornar2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.Equal(2, result.Count());
        }
    }
}
