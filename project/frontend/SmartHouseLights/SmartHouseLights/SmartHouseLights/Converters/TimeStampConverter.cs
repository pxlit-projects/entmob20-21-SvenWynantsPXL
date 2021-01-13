using System;
using System.Globalization;
using Xamarin.Forms;

namespace SmartHouseLights.Converters
{
    public class TimeStampConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double hoursOn = (double) value;
            var timespan = TimeSpan.FromHours(hoursOn);
            int hours = timespan.Hours;
            timespan = timespan.Subtract(TimeSpan.FromHours(hours));
            int minutes = timespan.Minutes;
            timespan = timespan.Subtract(TimeSpan.FromMinutes(minutes));
            int seconds = timespan.Seconds;

            return $"Hours: {hours}, Minutes: {minutes}, Seconds: {seconds}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}