using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TestNinja.Mocking;

using Moq;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class ProductTests
    {

        //right way
        [Test]
        public void GetPrice_GoldCustomer_Apply30PercentDiscount()
        {
            Product product = new Product();
            product.ListPrice = 100;

            float result = product.GetPrice(new Customer { IsGold = true });

            Assert.That(result, Is.EqualTo(70));
        }


        //wrong way -- using unnecessary mocks
        //[Test]
        //public void GetPrice_GoldCustomer_Apply30PercentDiscount2()
        //{
        //    var product = new Product { ListPrice = 100};
            
        //    var customer = new Mock<Customer>();
        //    customer.Setup(c => c.IsGold).Returns(true);

        //    var result = product.GetPrice(customer.Object);

        //    Assert.That(result, Is.EqualTo(70));
        //}
    }
}
