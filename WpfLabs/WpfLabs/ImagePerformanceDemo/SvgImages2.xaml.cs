using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SharpVectors.Converters;

namespace WpfLabs.ImagePerformanceDemo
{
    /// <summary>
    /// SvgImages.xaml 的交互逻辑
    /// Svg图片集合
    /// </summary>
    public partial class SvgImages2
    {
        private Stopwatch _stopwatch;

        public string SvgPath { get; } = "ImagePerformanceDemo/Resources/warning.svg";

        public SvgImages2(int count)
        {
            InitializeComponent();
            DataContext = this;

            GC.Collect();

            RecordStart();
            CreateImages(count);
        }

        private void CreateImages(int count)
        {
            var svgConverter = new SvgImageConverterExtension();

            for (int i = 0; i < count; i++)
            {
                var svgImage = new Image
                {
                    Width = 100,
                    Height = 100,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                BindingOperations.SetBinding(svgImage, Image.SourceProperty,
                    new Binding(nameof(SvgPath)) {Mode = BindingMode.OneWay, Converter = svgConverter});

                Container.Children.Add(svgImage);
            }
        }

        private void RecordStart()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            _stopwatch.Stop();
            Msg.Text = $"耗时：{_stopwatch.ElapsedMilliseconds}ms";
        }
    }
}
