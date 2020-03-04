using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopSimple.Converters;

namespace WPFDesktopSimpleTests.Converters
{
    [TestClass]
    public class VisibilityOnStringEmptyConverterTest
    {
        [TestMethod]
        public void CollapseVisibility_WhenNoValueProvided()
        {
            VisibilityOnStringEmptyConverter converter = new VisibilityOnStringEmptyConverter();
            string result = (string)converter.Convert(string.Empty, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual("Collapsed", result);
        }

        [TestMethod]
        public void ShowVisibility_WhenNoValueProvided()
        {
            VisibilityOnStringEmptyConverter converter = new VisibilityOnStringEmptyConverter();
            string result = (string)converter.Convert("anything", typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual("Visible", result);
        }
    }
}
