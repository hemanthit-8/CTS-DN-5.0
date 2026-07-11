using System;
using NUnit.Framework;

namespace UserManagerLib.Tests
{
    [TestFixture]
    public class UserTests
    {
        private User _user;

        [SetUp]
        public void Setup()
        {
            _user = new User();
        }

        [Test]
        public void ValidatePANCardNumber_NullValue_ThrowsNullReferenceException()
        {
            Assert.That(() => _user.ValidatePANCardNumber(null), Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void ValidatePANCardNumber_EmptyValue_ThrowsNullReferenceException()
        {
            Assert.That(() => _user.ValidatePANCardNumber(string.Empty), Throws.TypeOf<NullReferenceException>());
        }

        [TestCase("AB12345")]
        [TestCase("ABCDEFGHIJK")]
        public void ValidatePANCardNumber_LengthNot10_ThrowsFormatException(string panCard)
        {
            Assert.That(() => _user.ValidatePANCardNumber(panCard), Throws.TypeOf<FormatException>());
        }

        [Test]
        public void ValidatePANCardNumber_TenCharacterValue_ReturnsValid_HappyPath()
        {
            string actual = _user.ValidatePANCardNumber("ABCPD1234E");

            Assert.That(actual, Is.EqualTo("Valid"));
        }

        [Test]
        public void CreateUser_ValidPANCard_DoesNotThrow_HappyPath()
        {
            User newUser = new User { PANCardNo = "ABCPD1234E" };

            Assert.DoesNotThrow(() => _user.CreateUser(newUser));
        }

        [Test]
        public void CreateUser_NullPANCard_ThrowsNullReferenceException()
        {
            User newUser = new User { PANCardNo = null };

            Assert.That(() => _user.CreateUser(newUser), Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void CreateUser_InvalidLengthPANCard_ThrowsFormatException()
        {
            User newUser = new User { PANCardNo = "SHORT" };

            Assert.That(() => _user.CreateUser(newUser), Throws.TypeOf<FormatException>());
        }
    }
}
