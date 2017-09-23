using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        }

        public static string GetWaterMarkText(DependencyObject dependencyObject)
        {
            return (string) dependencyObject.GetValue(WaterMarkTextProperty);
        }

        public static void SetWaterMarkText(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(WaterMarkTextProperty, value);
        }
    }
}