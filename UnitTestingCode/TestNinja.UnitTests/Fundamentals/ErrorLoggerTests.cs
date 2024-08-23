using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    internal class ErrorLoggerTests
    {
        private ErrorLogger _errorLogger;

        [SetUp]
        public void SetUp()
        {
            _errorLogger = new ErrorLogger();
        }

        [Test]
        [TestCase("error desc", "error desc")]
        public void Log_WhenCalled_SetTheLastErrorProperty(string error, string expectedResult)
        {
            _errorLogger.Log(error);

            Assert.That(_errorLogger.LastError, Is.EqualTo(expectedResult));

        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            Assert.That(() => { _errorLogger.Log(error); }, Throws.ArgumentNullException);

            //Assert.That(() => { _errorLogger.Log(error); }, Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]

        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var id = Guid.Empty;
            _errorLogger.ErrorLogged += (sender, args) => { id = args; };
            _errorLogger.Log("a");
             
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
