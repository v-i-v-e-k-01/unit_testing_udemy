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
    internal class EmployeeControllerTests
    {
        EmployeeController _employeeController;
        Mock<IEmployeeStorage> _employeeStorage;

        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
            _employeeController = new EmployeeController(_employeeStorage.Object);
        }

        [Test]
        public void DeleteEmployee_EmployeeWithIDNotPresent_Returns()
        {
            var result = _employeeController.DeleteEmployee(1);

            _employeeStorage.Verify(s => s.DeleteEmployee(1));
        }
    }
}
