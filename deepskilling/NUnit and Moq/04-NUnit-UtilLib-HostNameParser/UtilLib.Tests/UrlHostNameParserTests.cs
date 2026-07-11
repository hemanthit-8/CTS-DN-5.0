using System;
using NUnit.Framework;

namespace UtilLib.Tests
{
    [TestFixture]
    public class UrlHostNameParserTests
    {
        private UrlHostNameParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new UrlHostNameParser();
        }

        // Execution path 1: valid http/https url -> host name is returned
        [TestCase("http://www.example.com/page", "www.example.com")]
        [TestCase("https://www.google.com/search", "www.google.com")]
        [TestCase("http://cognizant.com", "cognizant.com")]
        public void ParseHostName_ValidHttpOrHttpsUrl_ReturnsHostName(string url, string expected)
        {
            string actual = _parser.ParseHostName(url);

            Assert.That(actual, Is.EqualTo(expected));
        }

        // Execution path 2: any other protocol -> FormatException
        [TestCase("ftp://files.example.com")]
        [TestCase("www.example.com")]
        public void ParseHostName_NonHttpProtocol_ThrowsFormatException(string url)
        {
            Assert.That(() => _parser.ParseHostName(url), Throws.TypeOf<FormatException>());
        }
    }
}
