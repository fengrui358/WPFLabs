using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfLabs.ImagePerformanceDemo
{
    /// <summary>
    /// SvgImages.xaml 的交互逻辑
    /// Svg图片集合
    /// </summary>
    public partial class SvgImages : Window
    {
        public SvgImages(int count)
        {
            InitializeComponent();

            var sw = Stopwatch.StartNew();
            //for (int i = 0; i < 2; i++)
            //{
            //    Container.Children.Add(new Image
            //    {
            //        Source = new BitmapImage
            //        {
            //            UriSource =
            //                new Uri(
            //                    "pack://application:,,,/WpfLabs;component/ImagePerformanceDemo/Resources/Cheese-WF.png", UriKind.RelativeOrAbsolute)
            //        }
            //    });
            //}

            var bi = new BitmapImage(new Uri("pack://application:,,,/WpfLabs;component/ImagePerformanceDemo/Resources/Cheese-WF.png"));

            var img = new Image
            {
                Source = bi
            };
            Container.Children.Add(img);
            sw.Stop();
            Msg.Text = $"耗时：{sw.ElapsedMilliseconds}ms";
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
