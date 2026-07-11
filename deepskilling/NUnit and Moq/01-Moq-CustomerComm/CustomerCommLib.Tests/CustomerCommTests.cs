using Moq;
using NUnit.Framework;

namespace CustomerCommLib.Tests
{
    [TestFixture]
    public class CustomerCommTests
    {
        private Mock<IMailSender> _mockMailSender;
        private CustomerComm _customerComm;

        [OneTimeSetUp]
        public void Init()
        {
            _mockMailSender = new Mock<IMailSender>();

            // Configure the mock so SendMail() accepts any two strings and always returns true
            _mockMailSender
                .Setup(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _customerComm = new CustomerComm(_mockMailSender.Object);
        }

        [TestCase("cust123@abc.com", "Some Message")]
        [TestCase("customer@xyz.com", "Another Message")]
        public void SendMail_AnyTwoStringArguments_ReturnsTrue(string toAddress, string message)
        {
            bool result = _mockMailSender.Object.SendMail(toAddress, message);

            Assert.That(result, Is.True);
        }

        [Test]
        public void SendMailToCustomer_UsesMockedMailSender_ReturnsTrueWithoutSendingRealMail()
        {
            bool result = _customerComm.SendMailToCustomer();

            Assert.That(result, Is.True);
            _mockMailSender.Verify(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
