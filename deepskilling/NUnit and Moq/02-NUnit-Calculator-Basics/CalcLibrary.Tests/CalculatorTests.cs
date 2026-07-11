using NUnit.Framework;

namespace CalcLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private SimpleCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new SimpleCalculator();
        }

        [TearDown]
        public void Cleanup()
        {
            _calculator = null;
        }

        [TestCase(2, 3, 5)]
        [TestCase(-2, 3, 1)]
        [TestCase(0, 0, 0)]
        [TestCase(10.5, 4.5, 15)]
        public void Addition_TwoNumbers_ReturnsSum(double a, double b, double expected)
        {
            double actual = _calculator.Addition(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
