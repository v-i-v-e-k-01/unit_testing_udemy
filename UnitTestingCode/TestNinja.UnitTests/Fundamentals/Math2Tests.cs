using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestNinja.Fundamentals;

using NUnit.Framework;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class Math2Tests
    {
        private Math2 _math;
        [SetUp]
        public void SetUp()
        {
            _math = new Math2();
        }

        [Test]
        [Ignore("Just for some time")]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            //Act
            var result = _math.Add(1, 2);

            //Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        public void Max_WhenCalled_ReturnGreaterArgument(int a, int b, int expectedOutput)
        {
            //Act
            var result = _math.Max(a, b);

            //Assert
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUptoLimit()
        {
            var result = _math.GetOddNumbers(5);

            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(3));

            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
        }

        [TestCase(-1, new int[] { })]
        [TestCase(0, new int[] { })]
        public void GetOddNumbers_LimitLessThanOrEqualToZero_ReturnEmptyArray(int limit, int[] expectedOutput)
        {
            var result = _math.GetOddNumbers(limit);

            Assert.That(result, Is.Empty);
            //Assert.That(result, Is.Ordered);
            //Assert.That(result, Is.Unique);
        }


    }
}
