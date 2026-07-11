using System;
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

        [TestCase(10, 3, 7)]
        [TestCase(5, 5, 0)]
        [TestCase(-2, 3, -5)]
        public void Subtraction_TwoNumbers_ReturnsDifference(double a, double b, double expected)
        {
            double actual = _calculator.Subtraction(a, b);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(2, 3, 6)]
        [TestCase(-2, 3, -6)]
        [TestCase(0, 5, 0)]
        public void Multiplication_TwoNumbers_ReturnsProduct(double a, double b, double expected)
        {
            double actual = _calculator.Multiplication(a, b);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(10, 2, 5)]
        [TestCase(9, 3, 3)]
        [TestCase(-8, 4, -2)]
        public void Division_TwoNumbers_ReturnsQuotient(double a, double b, double expected)
        {
            double actual = _calculator.Division(a, b);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Division_ByZero_ThrowsArgumentException()
        {
            try
            {
                _calculator.Division(10, 0);
                Assert.Fail("Division by zero");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Second Parameter Can't be Zero", ex.Message);
            }
        }

        [Test]
        public void TestAddAndClear()
        {
            double result = _calculator.Addition(5, 10);
            Assert.AreEqual(15, result);
            Assert.AreEqual(15, _calculator.GetResult);

            _calculator.AllClear();

            Assert.AreEqual(0, _calculator.GetResult);
        }
    }
}
