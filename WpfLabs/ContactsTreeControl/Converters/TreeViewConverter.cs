using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfLabs.ContactsTreeControl.Converters
{
    /// <summary>
    /// 调整边距
    /// </summary>
    [ValueConversion(typeof(Thickness), typeof(Thickness))]
    public class TreeViewItemMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thickness = (Thickness)value;
            thickness.Left += 25;

            return thickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
