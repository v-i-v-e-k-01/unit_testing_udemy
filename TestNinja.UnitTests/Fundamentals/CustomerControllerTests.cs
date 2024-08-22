using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class CustomerControllerTests
    {
        CustomerController _customerController;

        [SetUp]
        public void SetUp()
        {
            _customerController = new CustomerController(); 
        }
        [Test]
        public void GetCustomer_IdIsZero_ReturnsNotFoundObject()
        {
            var result = _customerController.GetCustomer(0);

            Assert.That(result, Is.TypeOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOkObject()
        {
            var result = _customerController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>() ); // is Ok Object
            Assert.That(result, Is.InstanceOf<Ok>()); // is Ok Object or one of its derivatives
        }
    }
}
