using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TestNinja.Mocking;

using Moq;
using System.Net;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloaderMock;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void Setup()
        {
            _fileDownloaderMock = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloaderMock.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            _fileDownloaderMock.Setup(f => f.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.False);
        }

        [Test]
        public void DownloadInstaller_DownloadSucceds_ReturnsTrue()
        {

            var result = _installerHelper.DownloadInstaller("customer", "installer");

            Assert.That(result, Is.True);
        }

    }
}
