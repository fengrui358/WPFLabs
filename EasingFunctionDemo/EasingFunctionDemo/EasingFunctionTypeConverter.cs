using System;
using System.Globalization;
using System.Windows.Data;

namespace EasingFunctionDemo
{
    public class EasingFunctionTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Type)
            {
                return ((Type) value).Name;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
