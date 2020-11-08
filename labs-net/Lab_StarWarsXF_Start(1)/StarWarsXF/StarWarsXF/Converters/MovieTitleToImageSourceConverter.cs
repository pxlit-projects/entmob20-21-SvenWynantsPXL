using System;
using System.Globalization;
using Xamarin.Forms;

namespace StarWarsXF.Converters
{
    public class MovieTitleToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string source = value.ToString().Replace(' ', '_').ToLower();
            return source + ".jpg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}