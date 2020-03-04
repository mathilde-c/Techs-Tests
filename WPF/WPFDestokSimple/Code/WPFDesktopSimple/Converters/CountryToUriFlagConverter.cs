using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFDesktopSimple.Converters
{
    public class CountryToUriFlagConverter : IValueConverter
    {
        private readonly string flagRelativeBasePath = @"/Resources/Images/";
        private readonly string flagExtension = ".bmp";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var country = value as string;

            if (country == null || country == string.Empty)
            {
                return string.Empty;
            }

            return flagRelativeBasePath + country + flagExtension;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
