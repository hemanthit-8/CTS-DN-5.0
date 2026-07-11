using System.Collections;
using NUnit.Framework;

namespace SeasonsLib.Tests
{
    [TestFixture]
    public class SeasonTellerTests
    {
        private SeasonTeller _seasonTeller;

        [SetUp]
        public void Setup()
        {
            _seasonTeller = new SeasonTeller();
        }

        // --- Straightforward TestCaseSource: a static field of object[] ---
        private static readonly object[] SeasonCases =
        {
            new object[] { "February", "Spring" },
            new object[] { "March", "Spring" },
            new object[] { "April", "Summer" },
            new object[] { "May", "Summer" },
            new object[] { "June", "Summer" },
            new object[] { "July", "Monsoon" },
            new object[] { "August", "Monsoon" },
            new object[] { "September", "Monsoon" },
            new object[] { "October", "Autumn" },
            new object[] { "November", "Autumn" },
            new object[] { "December", "Winter" },
            new object[] { "January", "Winter" },
            new object[] { "Foo", "Invalid Season" },
        };

        [TestCaseSource(nameof(SeasonCases))]
        public void DisplaySeasonBy_GivenMonth_ReturnsExpectedSeason(string month, string expectedSeason)
        {
            string actual = _seasonTeller.DisplaySeasonBy(month);

            Assert.That(actual, Is.EqualTo(expectedSeason));
        }

        // --- Alternate way: a method yielding TestCaseData, driving the return value directly ---
        private static IEnumerable SeasonCaseData()
        {
            yield return new TestCaseData("February").Returns("Spring");
            yield return new TestCaseData("June").Returns("Summer");
            yield return new TestCaseData("September").Returns("Monsoon");
            yield return new TestCaseData("November").Returns("Autumn");
            yield return new TestCaseData("January").Returns("Winter");
            yield return new TestCaseData("NotAMonth").Returns("Invalid Season");
        }

        [TestCaseSource(nameof(SeasonCaseData))]
        public string DisplaySeasonBy_UsingTestCaseDataReturns_MatchesExpectedSeason(string month)
        {
            return _seasonTeller.DisplaySeasonBy(month);
        }
    }
}
