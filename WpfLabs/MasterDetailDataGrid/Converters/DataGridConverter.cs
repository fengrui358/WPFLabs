using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WpfLabs.MasterDetailDataGrid.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class DataGridRowDetailVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //修改默认的展示子类容的方式
            //目前的需求只能由Button来改变

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool) value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}