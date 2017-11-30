using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfLabs.FontFamilyDemo
{
    [ValueConversion(typeof(double), typeof(TextFormattingMode))]
    public class TextFormattingModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                if ((double) value >= 15)
                {
                    return TextFormattingMode.Ideal;
                }
                else
                {
                    return TextFormattingMode.Display;
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
