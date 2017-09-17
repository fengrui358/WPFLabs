using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfLabs.DrawingDemo
{
    public class ControlRowConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var windowHeight = (double) values[0];
            var controlRowHeight = (GridLength) values[1];

            var heigth = windowHeight - controlRowHeight.Value;
            return heigth >= 0 ? heigth : Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
