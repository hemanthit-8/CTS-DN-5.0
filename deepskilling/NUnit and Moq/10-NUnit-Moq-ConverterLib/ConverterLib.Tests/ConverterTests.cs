using CurrencyConverterApp;
using Moq;
using NUnit.Framework;

namespace ConverterLib.Tests
{
    [TestFixture]
    public class ConverterTests
    {
        private Mock<IDollarToEuroExchangeRateFeed> _mockExchangeRateFeed;
        private Converter _converter;

        [SetUp]
        public void Setup()
        {
            _mockExchangeRateFeed = new Mock<IDollarToEuroExchangeRateFeed>();
            _converter = new Converter(_mockExchangeRateFeed.Object);
        }

        // USDToEuro depends on an external service (IDollarToEuroExchangeRateFeed) that
        // can't be exercised in a unit test, so it's mocked with Moq.
        [TestCase(100, 0.90, 90)]
        [TestCase(50, 0.85, 42.5)]
        [TestCase(0, 0.90, 0)]
        public void USDToEuro_GivenMockedExchangeRate_ReturnsConvertedAmount(double dollars, double rate, double expected)
        {
            _mockExchangeRateFeed.Setup(f => f.GetActualUSDollarValue()).Returns(rate);

            double actual = _converter.USDToEuro(dollars);

            Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void USDToEuro_CallsExchangeRateFeed_ExactlyOnce()
        {
            _mockExchangeRateFeed.Setup(f => f.GetActualUSDollarValue()).Returns(0.9);

            _converter.USDToEuro(100);

            _mockExchangeRateFeed.Verify(f => f.GetActualUSDollarValue(), Times.Once);
        }

        [TestCase(0, 273.15)]
        [TestCase(100, 373.15)]
        public void CelsiusToKelvin_GivenCelsius_ReturnsKelvin(double celsius, double expected)
        {
            double actual = _converter.CelsiusToKelvin(celsius);

            Assert.That(actual, Is.EqualTo(expected).Within(0.01));
        }

        [TestCase(10, 22.05)]
        public void KilogramToPound_GivenKilogram_ReturnsPound(double kilogram, double expected)
        {
            double actual = _converter.KilogramToPound(kilogram);

            Assert.That(actual, Is.EqualTo(expected).Within(0.01));
        }

        [TestCase(10, 6.215)]
        public void KilometerToMile_GivenKilometer_ReturnsMile(double kilometer, double expected)
        {
            double actual = _converter.KilometerToMile(kilometer);

            Assert.That(actual, Is.EqualTo(expected).Within(0.01));
        }

        [TestCase(10, 2.642)]
        public void LiterToGallon_GivenLiter_ReturnsGallon(double liter, double expected)
        {
            double actual = _converter.LiterToGallon(liter);

            Assert.That(actual, Is.EqualTo(expected).Within(0.01));
        }
    }
}
