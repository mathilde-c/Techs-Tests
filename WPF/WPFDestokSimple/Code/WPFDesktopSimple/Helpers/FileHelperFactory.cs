using System;
using System.Collections.Generic;

namespace WPFDesktopSimple.Helpers
{
    public class FileHelperFactory : IFileHelperFactory
    {
        public IFileHelper GetFileHelper(FileFormatAccepted format)
        {
            IFileHelper helper;
            switch (format)
            {
                case FileFormatAccepted.Xml:
                    helper = new FileHelperXml();
                    break;
                default:
                    throw new ArgumentException(format.ToString("g"));
            }

            return helper;
        }

        public string GetFilterFormat(FileFormatAccepted formats)
        {
            IList<string> filters = new List<string>();

            if ((formats & FileFormatAccepted.Xml) == FileFormatAccepted.Xml)
            {
                filters.Add("XML (*.xml)|*.xml");
            }

            if ((formats & FileFormatAccepted.Text) == FileFormatAccepted.Text)
            {
                filters.Add("TXT (*.txt)|*.txt");
            }

            if ((formats & FileFormatAccepted.All) == FileFormatAccepted.All)
            {
                filters.Add("ALL (*.*)|*.*");
            }

            return string.Join("|", filters);
        }
    }
}
