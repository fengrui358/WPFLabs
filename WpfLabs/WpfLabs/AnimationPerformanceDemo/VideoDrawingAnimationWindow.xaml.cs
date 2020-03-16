using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfLabs.AnimationPerformanceDemo
{
    /// <summary>
    /// VideoDrawingAnimationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class VideoDrawingAnimationWindow : Window
    {
        private bool _isSwitch;

        public VideoDrawingAnimationWindow()
        {
            InitializeComponent();
        }

        private void VideoDrawingAnimationWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var player = new System.Windows.Media.MediaPlayer();
            player.MediaEnded += PlayerOnMediaEnded;

            player.Open(new Uri(@"AnimationPerformanceDemo\Assets\1.mp4", UriKind.RelativeOrAbsolute));

            var videoDrawing = new VideoDrawing {Rect = new Rect(0, 0, 1920, 1080), Player = player};

            var brush = new DrawingBrush(videoDrawing);
            Background = brush;

            player.Play();
        }

        private void PlayerOnMediaEnded(object sender, EventArgs e)
        {
            var player = ((System.Windows.Media.MediaPlayer)sender);

            if (!_isSwitch)
            {
                _isSwitch = true;

                player.Open(new Uri(@"AnimationPerformanceDemo\Assets\2.mp4", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            else
            {
                player.Position = TimeSpan.Zero;
                player.Play();
            }
        }
    }
}