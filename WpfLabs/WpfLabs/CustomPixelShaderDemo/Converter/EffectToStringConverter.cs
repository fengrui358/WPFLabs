using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfLabs.CustomPixelShaderDemo.Converter
{
    public class EffectToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.GetType().Name;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
