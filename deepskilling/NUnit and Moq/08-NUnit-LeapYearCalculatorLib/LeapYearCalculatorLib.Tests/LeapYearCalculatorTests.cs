using NUnit.Framework;

namespace LeapYearCalculatorLib.Tests
{
    [TestFixture]
    public class LeapYearCalculatorTests
    {
        private LeapYearCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new LeapYearCalculator();
        }

        [TestCase(2000, 1)]
        [TestCase(2400, 1)]
        [TestCase(2020, 1)]
        public void IsLeapYear_LeapYear_ReturnsOne(int year, int expected)
        {
            int actual = _calculator.IsLeapYear(year);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1900, 0)]
        [TestCase(2021, 0)]
        [TestCase(2100, 0)]
        public void IsLeapYear_NonLeapYear_ReturnsZero(int year, int expected)
        {
            int actual = _calculator.IsLeapYear(year);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1752, -1)]
        [TestCase(10000, -1)]
        [TestCase(0, -1)]
        public void IsLeapYear_YearOutsideValidRange_ReturnsMinusOne(int year, int expected)
        {
            int actual = _calculator.IsLeapYear(year);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1753, 0)]
        [TestCase(9999, 0)]
        public void IsLeapYear_BoundaryValidYears_DoesNotReturnMinusOne(int year, int expected)
        {
            int actual = _calculator.IsLeapYear(year);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
