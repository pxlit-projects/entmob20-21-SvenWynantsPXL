using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHouseLights.Converters
{
    public class HomeLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string name)
            {
                return $"Welcome to the smart home app, {name}!";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}