using System;

namespace WPFDesktopSimple.Helpers
{
    public interface IFileHelperFactory
    {
        IFileHelper GetFileHelper(FileFormatAccepted format);
        string GetFilterFormat(FileFormatAccepted formats);
    }

    [Flags]
    public enum FileFormatAccepted
    {
        Xml,
        Text,
        All = Xml | Text
    }
}
