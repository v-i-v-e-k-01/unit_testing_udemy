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
    internal class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_StoreTheOrder()
        {
            var storage = new Mock<IStorage>();

            var service = new OrderService(storage.Object);
            var order = new Order();
            service.PlaceOrder(order);

            storage.Verify(s => s.Store(order));

        }
    }
}
