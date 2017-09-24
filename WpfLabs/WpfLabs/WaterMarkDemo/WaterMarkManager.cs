using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfLabs.Helper;

namespace WpfLabs.WaterMarkDemo
{
    public class WaterMarkManager : DependencyObject
    {
        public static readonly DependencyProperty WaterMarkTextProperty = DependencyProperty.RegisterAttached(
            "WaterMarkText", typeof(string), typeof(WaterMarkManager), new PropertyMetadata(default(string), WaterMarkTextCallback));

        private static void WaterMarkTextCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            DebugWriterHelper.WriterLine("WaterMarkTextCallback");

            var textBox = (TextBox) dependencyObject;
            if (!textBox.IsLoaded)
            {
                textBox.GotFocus += TextBoxOnGotFocus;
                textBox.LostFocus += TextBoxOnLostFocus;
                textBox.TextChanged += TextBoxOnTextChanged;
                textBox.Unloaded += TextBoxOnUnloaded;
            }
        }

        private static void TextBoxOnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var textBox = (TextBox) sender;

            textBox.Unloaded += TextBoxOnUnloaded;
            textBox.GotFocus += TextBoxOnGotFocus;
            textBox.LostFocus += TextBoxOnLostFocus;
            textBox.TextChanged += TextBoxOnTextChanged;
        }

        private static void TextBoxOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            throw new NotImplementedException();
        }

        private static void TextBoxOnGotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            throw new NotImplementedException();
        }

        private static void TextBoxOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            throw new NotImplementedException();
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static string GetWaterMarkText(DependencyObject dependencyObject)
        {
            return (string) dependencyObject.GetValue(WaterMarkTextProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetWaterMarkText(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(WaterMarkTextProperty, value);
        }
    }
}