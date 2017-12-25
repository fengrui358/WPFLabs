using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfLabs.ImagePerformanceDemo
{
    /// <summary>
    /// FontImages.xaml 的交互逻辑
    /// 字体图片集合
    /// </summary>
    public partial class FontImages
    {
        private Stopwatch _stopwatch;

        public FontImages(int count)
        {
            InitializeComponent();

            GC.Collect();

            RecordStart();
            CreateImages(count);
        }

        private void CreateImages(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var txtIcon = new TextBlock
                {
                    Text = "\u3433",
                    Foreground = Brushes.Red,
                    FontSize = 80,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                Container.Children.Add(txtIcon);
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
