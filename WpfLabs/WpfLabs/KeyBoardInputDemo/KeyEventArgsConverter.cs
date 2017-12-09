using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfLabs.KeyBoardInputDemo
{
    public class KeyEventArgsConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var keyEventArgs = value as KeyEventArgs;
            if (keyEventArgs != null)
            {
                return
                    $"Key:{keyEventArgs.Key}--KeyStates:{keyEventArgs.KeyStates}--Modifiers:{keyEventArgs.KeyboardDevice.Modifiers}";
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
