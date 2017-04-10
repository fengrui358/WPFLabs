using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfLabs.Timer.Converters
{
    /// <summary>
    /// 总秒数修改为时间字符串
    /// </summary>
    [ValueConversion(typeof(long), typeof(string))]
    public class TimerStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var totalSeconds = (long) value;
            var timeSpan = TimeSpan.FromSeconds(totalSeconds);

            return timeSpan.ToString("c");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
