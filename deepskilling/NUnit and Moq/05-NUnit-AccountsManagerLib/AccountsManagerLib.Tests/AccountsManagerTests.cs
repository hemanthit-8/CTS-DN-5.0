using System;
using NUnit.Framework;

namespace AccountsManagerLib.Tests
{
    [TestFixture]
    public class AccountsManagerTests
    {
        private AccountsManager _accountsManager;

        [SetUp]
        public void Setup()
        {
            _accountsManager = new AccountsManager();
        }

        [TestCase("user_11", "secret@user11", "Welcome user_11!!!")]
        [TestCase("user_22", "secret@user22", "Welcome user_22!!!")]
        public void ValidateUser_ValidCredentials_ReturnsWelcomeMessage(string userId, string password, string expected)
        {
            string actual = _accountsManager.ValidateUser(userId, password);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("user_11", "wrongPassword")]
        [TestCase("unknown_user", "secret@user11")]
        public void ValidateUser_InvalidCredentials_ReturnsInvalidMessage(string userId, string password)
        {
            string actual = _accountsManager.ValidateUser(userId, password);

            Assert.That(actual, Is.EqualTo("Invalid user id/password"));
        }

        // Note: the AccountsManagerLib source throws FormatException (not ArgumentException as the
        // brief describes in prose) when either credential is missing - tests follow the actual code.
        [TestCase(null, "secret@user11")]
        [TestCase("user_11", null)]
        [TestCase("", "")]
        public void ValidateUser_MissingUserIdOrPassword_ThrowsFormatException(string userId, string password)
        {
            Assert.That(() => _accountsManager.ValidateUser(userId, password), Throws.TypeOf<FormatException>());
        }
    }
}
