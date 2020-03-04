using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopSimple.Converters;

namespace WPFDesktopSimpleTests.Converters
{
    [TestClass]
    public class CountryToUriFlagConverterTest
    {
        [DataTestMethod]
        [DataRow("CRT", "/Resources/Images/CRT.bmp")]
        [DataRow("PTSDeed", "/Resources/Images/PTSDeed.bmp")]
        public void ProvidesRelativeUriForBMPImage_BasedOnCountryValue(string country, string expectedOutput)
        {
            CountryToUriFlagConverter converter = new CountryToUriFlagConverter();
            string output = (string)converter.Convert(country, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual(output, expectedOutput);
        }
        public void ReturnsNothing_WhenCountryIsNothing()
        {
            CountryToUriFlagConverter converter = new CountryToUriFlagConverter();
            string output = (string)converter.Convert(string.Empty, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual(string.Empty, output);
        }
    }
}
