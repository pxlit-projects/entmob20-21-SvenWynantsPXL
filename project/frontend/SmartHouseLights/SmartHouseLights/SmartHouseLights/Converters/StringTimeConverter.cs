using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHouseLights.Converters
{
    public class StringTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string date = (string)value;
            if (date.Equals(""))
            {
                return new TimeSpan();
            } 

            DateTime time = DateTime.Parse(date);

            return time.TimeOfDay;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (TimeSpan) value;
            if (Math.Abs(time.TotalMinutes - new TimeSpan().TotalMinutes) < 0.1)
            {
                return "";
            }
            return new DateTime(2020, 1, 1, time.Hours, time.Minutes, time.Seconds);
        }
    }
}