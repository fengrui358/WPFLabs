using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfLabs.NewCallAnimation
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                var timeSpan = (TimeSpan) value;

                if (timeSpan.Hours > 0)
                {
                    return string.Format("{0}:{1}:{2}", timeSpan.Hours.ToString().PadLeft(2, '0'),
                        timeSpan.Minutes.ToString().PadLeft(2, '0'), timeSpan.Seconds.ToString().PadLeft(2, '0'));
                }
                else
                {
                    return string.Format("{0}:{1}", timeSpan.Minutes.ToString().PadLeft(2, '0'),
                        timeSpan.Seconds.ToString().PadLeft(2, '0'));
                }
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
