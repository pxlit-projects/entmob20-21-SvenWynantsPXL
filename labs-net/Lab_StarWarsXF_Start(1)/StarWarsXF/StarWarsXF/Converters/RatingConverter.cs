using System;
using System.Globalization;
using Xamarin.Forms;

namespace StarWarsXF.Converters
{
    public class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double valueOnScaleOfTen = System.Convert.ToDouble(value);
            return valueOnScaleOfTen / 10.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            value = (double) value * 10;
            float retValue = System.Convert.ToSingle(value);

            return retValue;
        }
    }
}