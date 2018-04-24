using System;
using System.Globalization;
using System.Windows.Data;

namespace SpeechDemo
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = (TimeSpan) value;
            if (timeSpan != TimeSpan.Zero)
            {
                return $"转换耗时{timeSpan.Milliseconds}ms";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
