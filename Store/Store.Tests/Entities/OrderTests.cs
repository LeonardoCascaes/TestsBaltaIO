using Store.Domain.Entities;
using Store.Domain.Enums;
using System;
using Xunit;

namespace Store.Tests.Entities
{
    public class OrderTests
    {
        private readonly Customer _customer;
        private readonly Discount _discount;
        private readonly Product _product;

        public OrderTests()
        {
            _customer = new Customer("Leonardo", "leonardo@hotmail.com");
            _product = new Product("Produto 1", 10, true);
            _discount = new Discount(10, DateTime.Now.AddDays(5));
        }

        [Fact]
        public void DadoUmNovoPedidoValidoEleDeveGerarUmNumeroCom8Caracteres()
        {
            var order = new Order(_customer, 0, null);
            Assert.Equal(8, order.Number.Length);
        }
        
        [Fact]
        public void DadoUmNovoPedidoSeuStatusDeveSerAguardandoPagamento()
        {
            var order = new Order(_customer, 0, null);
            Assert.Equal(EOrderStatus.WaitingPayment, order.Status);
        }

        [Fact]
        public void DadoUmPagamentoNoPedidoSeuStatusDeveSerAguardandoEntrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);
            order.Pay(10);
            Assert.Equal(EOrderStatus.WaitingDelivery, order.Status);
        }

        [Fact]
        public void DadoUmPedidoCanceladoSeuStatusDeveSerCancelado()
        {
            var order = new Order(_customer, 0, null);
            order.Cancel();
            Assert.Equal(EOrderStatus.Canceled, order.Status);
        }

        [Fact]
        public void DadoUmNovoItemSemProdutoOMesmoNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(null, 1);
            Assert.Empty(order.Items);
        }

        [Fact]
        public void DadoUmItemComQuantidadeZeroOuMenorOMesmoNaoDeveSerAdicionado()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 0);
            Assert.Equal(0, order.Items.Count);
        }

        [Fact]
        public void DadoUmNovoPedidoValidoSeuTotalDeveSer50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);
            Assert.Equal(50, order.Total());
        }

        [Fact]
        public void DadoUmDescontoExpiradoOValorDoPedidoDeveSer60()
        {
            var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5));
            var order = new Order(_customer, 10, expiredDiscount);
            order.AddItem(_product, 5);
            Assert.Equal(60, order.Total());
        }

        [Fact]
        public void DadoUmDescontoInvalidoOValorDoPedidoDeveSer60()
        {
            var order = new Order(_customer, 20, null);
            order.AddItem(_product, 4);
            Assert.Equal(60, order.Total());
        }

        [Fact]
        public void DadoUmDescontoDe10OValorDoPedidoDeveSer50()
        {
            var order = new Order(_customer, 0, _discount);
            order.AddItem(_product, 6);
            Assert.Equal(50, order.Total());
        }

        [Fact]
        public void DadoUmaTaxaDeEntregaDe10OValorDoPedidoDeveSer80()
        {
            var order = new Order(_customer, 40, _discount);
            order.AddItem(_product, 5);
            Assert.Equal(80, order.Total());
        }

        [Fact]
        public void DadoUmPedidoSerClienteOMesmoDeveSerInvalido()
        {
            var order = new Order(null, 40, _discount);
            Assert.False(order.Valid);
        }
    }
}
