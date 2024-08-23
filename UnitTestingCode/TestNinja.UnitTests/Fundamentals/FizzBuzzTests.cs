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
    internal class FizzBuzzTests
    {

        [Test]
        [TestCase(6, "Fizz")]
        [TestCase(10, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(1, "1")]
        public void GetOutPut_WhenCalled_ReturnsAppropriateString(int number, string expectedOutput)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
