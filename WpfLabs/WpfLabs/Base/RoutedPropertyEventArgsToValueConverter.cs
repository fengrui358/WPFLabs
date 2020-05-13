using System;
using FrHello.NetLib.Core.Reflection;
using GalaSoft.MvvmLight.Command;

namespace WpfLabs.Base
{
    public class RoutedPropertyEventArgsToValueConverter<T> : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            try
            {
                var valueType = value.GetType();

                if (valueType.GetGenericTypeDefinition() == typeof(RoutedPropertyEventArgs<>))
                {
                    if (typeof(T) == typeof(object) && parameter is Type type)
                    {
                        return TypeHelper.TryChangeType(((dynamic) value).Value, type);
                    }
                    else
                    {
                        return TypeHelper.TryChangeType(((dynamic) value).Value, typeof(T));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return value;
        }
    }

    public class EventArgsToValueConverterProvider
    {
        public static RoutedPropertyEventArgsToValueConverter<int> ConvertToInt { get; } = new RoutedPropertyEventArgsToValueConverter<int>();

        public static RoutedPropertyEventArgsToValueConverter<string> ConvertToString { get; } = new RoutedPropertyEventArgsToValueConverter<string>();

        public static RoutedPropertyEventArgsToValueConverter<double> ConvertToDouble { get; } = new RoutedPropertyEventArgsToValueConverter<double>();

        public static RoutedPropertyEventArgsToValueConverter<object> Converter { get; } = new RoutedPropertyEventArgsToValueConverter<object>();
    }
}
