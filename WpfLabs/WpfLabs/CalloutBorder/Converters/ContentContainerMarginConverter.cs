using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfLabs.CalloutBorder.Converters
{
    public class ContentContainerMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var padding = (Thickness) values[0];
            var border = (Thickness) values[1];

            return new Thickness(padding.Left + border.Left, padding.Top + border.Top, padding.Right + border.Right, padding.Bottom + border.Bottom);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
