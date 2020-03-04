using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFDesktopSimple.Converters
{
    public class VisibilityOnStringEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value != string.Empty ? "Visible" : "Collapsed";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
