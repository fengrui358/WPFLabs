using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfLabs.CustomPixelShaderDemo.Converter
{
    public class PaintSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var imageSource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "paint", $"{value}.jpg");
                if (File.Exists(imageSource))
                {
                    return imageSource;
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
