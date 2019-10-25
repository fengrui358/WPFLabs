using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace KeyBoardDemo
{
    public class KeyEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is KeyEventArgs keyEventArgs)
            {
                if (keyEventArgs.KeyboardDevice.Modifiers == ModifierKeys.None)
                {
                    return $"{keyEventArgs.Key}（Virtual Key:{KeyInterop.VirtualKeyFromKey(keyEventArgs.Key)}）";
                }

                return
                    $"{keyEventArgs.KeyboardDevice.Modifiers} + {keyEventArgs.Key}（Virtual Key:{KeyInterop.VirtualKeyFromKey(keyEventArgs.Key)}）";
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
