using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfLabs.ImagePerformanceDemo
{
    /// <summary>
    /// PathImages.xaml 的交互逻辑
    /// Path图片集合
    /// </summary>
    public partial class PathImages
    {
        private Stopwatch _stopwatch;

        public PathImages(int count)
        {
            InitializeComponent();

            RecordStart();
            CreateImages(count);
        }

        private void CreateImages(int count)
        {
            var drawingImage = (DrawingImage) Resources["DrawingImage"];

            for (int i = 0; i < count; i++)
            {
                Container.Children.Add(new Image
                {
                    Source = drawingImage,
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
