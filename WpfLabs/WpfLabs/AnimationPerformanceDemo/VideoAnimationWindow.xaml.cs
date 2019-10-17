using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfLabs.AnimationPerformanceDemo
{
    /// <summary>
    /// VideoAnimationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class VideoAnimationWindow : Window
    {
        private static readonly Random _random = new Random();
        private int _firstVideoPlayCount;
        private bool _hasChanged;

        public VideoAnimationWindow()
        {
            InitializeComponent();
        }

        private void VideoAnimationWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MyMediaElement.LoadedBehavior = MediaState.Manual;
            MyMediaElement.MediaOpened += MyMediaElementOnMediaOpened;
            MyMediaElement.MediaEnded += MediaElementOnMediaEnded;

            MyMediaElement2.LoadedBehavior = MediaState.Manual;
            MyMediaElement2.MediaOpened += MyMediaElementOnMediaOpened;
            MyMediaElement2.MediaEnded += MediaElementOnMediaEnded2;

            MyMediaElement.Source = new Uri(@"AnimationPerformanceDemo\Assets\1.mp4", UriKind.RelativeOrAbsolute);
            MyMediaElement2.Source = new Uri(@"AnimationPerformanceDemo\Assets\2.mp4", UriKind.RelativeOrAbsolute);

            //await Task.Delay(_random.Next(0, 1000));

            MyMediaElement.Play();
            MyMediaElement2.Play();
        }

        private void MyMediaElementOnMediaOpened(object sender, RoutedEventArgs e)
        {
            //跳过黑屏
            ((MediaElement) sender).Position = TimeSpan.FromSeconds(0.2);
        }

        private void MediaElementOnMediaEnded(object sender, RoutedEventArgs routedEventArgs)
        {
            var mediaElement = (MediaElement) sender;

            if (_firstVideoPlayCount == 0 && !_hasChanged)
            {
                _hasChanged = true;

                mediaElement.Stop();
                MyMediaElement2.Visibility = Visibility.Visible;
                mediaElement.Visibility = Visibility.Collapsed;
            }
            else
            {
                mediaElement.Stop();
                mediaElement.Play();

                _firstVideoPlayCount++;
            }
        }

        private void MediaElementOnMediaEnded2(object sender, RoutedEventArgs routedEventArgs)
        {
            var mediaElement = (MediaElement)sender;

            mediaElement.Stop();
            mediaElement.Play();
        }
    }
}
