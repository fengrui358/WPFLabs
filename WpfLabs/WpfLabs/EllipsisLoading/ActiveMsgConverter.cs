using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfLabs.EllipsisLoading
{
    public class ActiveMsgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value)
            {
                return "停止";
            }
            else
            {
                return "激活";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
