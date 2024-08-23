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
    internal class HtmlFormatterTests
    {
        private HtmlFormatter _formatter;
        [SetUp]
        public void SetUp()
        {
            _formatter = new HtmlFormatter();            
        }
        [Test]
        public void FormatAsBold_WhenCalled_ReturnsTheStringWithStrongElement()
        {
            var result = _formatter.FormatAsBold("abc");

            //specific
            //Assert.That(result, Is.EqualTo("<strong>{abc}</strong>"));

            //generic
            Assert.That(result, Does.StartWith("<strong>"));

            ////More general
            Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain("abc"));
        }


    }
}
