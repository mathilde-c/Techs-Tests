using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopSimple.Converters;

namespace WPFDesktopSimpleTests.Converters
{
    [TestClass]
    public class NetTimeValueConverterTest
    {
        [DataTestMethod]
        [DataRow(0, "0")]
        [DataRow(1, "1")]
        [DataRow(1.2, "1,2")]
        public void ConvertNumericalValueToStringEquivalent_WhenProvided(double value, string expectedOutput)
        {
            NetTimeValueConverter converter = new NetTimeValueConverter();
            string result = (string)converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void RetunrsEmptyPattern_WhenNoValueProvided()
        {
            NetTimeValueConverter converter = new NetTimeValueConverter();
            string result = (string)converter.Convert(null, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual(" - ", result);
        }
    }
}
