using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHouseLights.Converters
{
    public class ButtonToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool state = (bool)value;

            if (state)
            {
                return "Turn off";
            }

            return "Turn on";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}