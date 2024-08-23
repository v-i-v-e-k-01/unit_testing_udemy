using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using TestNinja.Mocking;

using Moq;
using System.IO;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IEmailSender> _mailSender;
        private Mock<IStatementGenereator> _statementGenerator;
        private Mock<IXtraMessageBox> _xtraMessageBox;
        private HousekeeperService _housekeeperService;
        readonly DateTime _statementDate = DateTime.Now;
        private Housekeeper _houseKeeper;
        string _statementFileName;


        [SetUp]
        public void Setup()
        {
            _houseKeeper = new Housekeeper
            {
                Email = "abcde",
                FullName = "ABC",
                Oid = 14,
                StatementEmailBody = null,
            };
            _unitOfWork = new Mock<IUnitOfWork>();
            _mailSender = new Mock<IEmailSender>();
            _statementGenerator = new Mock<IStatementGenereator>();
            _statementFileName = "fileName";
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);
            _xtraMessageBox = new Mock<IXtraMessageBox>();

            _housekeeperService = new HousekeeperService(
                _unitOfWork.Object,
                _mailSender.Object,
                _statementGenerator.Object,
                _xtraMessageBox.Object);


            _unitOfWork.Setup(u => u.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
               _houseKeeper
            }.AsQueryable());

        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {

            _housekeeperService.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));

        }

        [Test]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public void SendStatementEmails_HouseKeeperEmailIsNullOrWhiteSpaceOrEmptyString_ShouldNotGenerateStatements(string email)
        {
            _houseKeeper.Email = email;

            _housekeeperService.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public void SendStatementEmails_StatementFileIsNullOrWhiteSpace_ShouldNotSendEmails(string statementGeneratorResponse)
        {
            _statementFileName = statementGeneratorResponse;

            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _housekeeperService.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _mailSender.Setup(ms => ms.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            _housekeeperService.SendStatementEmails(_statementDate);

            _xtraMessageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerifyEmailSent()
        {
            _mailSender.Verify(m => m.EmailFile(_houseKeeper.Email, _houseKeeper.StatementEmailBody, _statementFileName, It.IsAny<string>()));
        }

        private void VerifyEmailNotSent()
        {
            _mailSender.Verify(m => m.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }

}
