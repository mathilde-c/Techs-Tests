using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFDesktopSimple.Helpers;

namespace WPFDesktopSimpleTests.Helpers
{
    [TestClass]
    public class FileHelperFactoryTest
    {
        [TestMethod]
        public void ProvidesAnXMLFileHelper_WhenRequestedWithXMLFormatType()
        {
            FileHelperFactory factory = new FileHelperFactory();
            IFileHelper helper = factory.GetFileHelper(FileFormatAccepted.Xml);

            Assert.IsInstanceOfType(helper, typeof(FileHelperXml));
        }

        [TestMethod]
        public void ProvidesAnXMLFileFiler_WhenRequestedWithXMLFormatType()
        {
            FileHelperFactory factory = new FileHelperFactory();
            string filter = factory.GetFilterFormat(FileFormatAccepted.Xml);

            Assert.AreEqual("XML (*.xml)|*.xml", filter);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsAnException_WhenProvidedWithMultiFormat()
        {
            FileHelperFactory factory = new FileHelperFactory();
            IFileHelper helper = factory.GetFileHelper(FileFormatAccepted.All);
            // Assert - Expects exception
        }
    }
}
