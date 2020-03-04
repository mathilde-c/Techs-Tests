using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopSimple.Converters;
using ThirdPartySimulator;

namespace WPFDesktopSimpleTests.Converters
{
    [TestClass]
    public class IrmToColorConverterTest
    {
        [DataTestMethod]
        [DataRow(EndingEventType.None, "Black")]
        [DataRow(EndingEventType.Abandon, "Orange")]
        [DataRow(EndingEventType.Fault, "Red")]
        [DataRow(EndingEventType.Withdrawn, "Navy")]
        public void MapsEndingEventTypeToExpectedColor_WhenEndingEventTypeKnown(EndingEventType irm, string expectedOutput)
        {
            EndingEventToColorConverter converter = new EndingEventToColorConverter();
            var result = (string)converter.Convert(irm.ToString("g"), typeof(string), null, CultureInfo.InvariantCulture);

            Assert.AreEqual(expectedOutput, result);
        }

        [TestMethod]
        public void MapsEndingEventTypeToBlackColor_WhenEndingEventTypeUnknownOrInvalid()
        {
            EndingEventToColorConverter converter = new EndingEventToColorConverter();
            var result = (string)converter.Convert("invalid", typeof(EndingEventType), null, CultureInfo.InvariantCulture);

            Assert.AreEqual("Black", result);
        }
    }
}
