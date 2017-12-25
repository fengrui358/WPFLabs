using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfLabs.ImagePerformanceDemo
{
    /// <summary>
    /// PngImages.xaml 的交互逻辑
    /// 普通的Png图片集合
    /// </summary>
    public partial class PngImages
    {
        private Stopwatch _stopwatch;

        public PngImages(int count)
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
                Container.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(
                        "pack://application:,,,/WpfLabs;component/ImagePerformanceDemo/Resources/timer.png")),
                    Stretch = Stretch.None
                });
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