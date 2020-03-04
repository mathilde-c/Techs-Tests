using System;
using System.Windows.Data;

namespace WPFDesktopSimple.Converters
{
	public class NetTimeValueConverter : IValueConverter
	{
		private const string EMPTY = " - ";
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string valueAsString = value?.ToString() ?? string.Empty;
			return valueAsString != string.Empty ? valueAsString : EMPTY;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (string)value == EMPTY ? string.Empty : value;
		}
	}
}
