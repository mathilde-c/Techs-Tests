
using System;
using System.Globalization;
using System.Windows.Data;
using ThirdPartySimulator;

namespace WPFDesktopSimple.Converters
{
    public class EndingEventToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EndingEventType endingEventVal;
            if (!Enum.TryParse(value as string, out endingEventVal)) {
                endingEventVal = EndingEventType.None;
            } 

            switch (endingEventVal)
            {
                case EndingEventType.Withdrawn:
                    return "Navy";
                case EndingEventType.Fault:
                    return "Red";
                case EndingEventType.Abandon:
                    return "Orange";
                default:
                    return "Black";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
