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
using WpfLabs.NewCallAnimation;

namespace WpfLabs.AnimationPerformanceDemo
{
    /// <summary>
    /// AnimationPerformanceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationPerformanceWindow : Window
    {
        public int Count { get; } = 2;

        public AnimationPerformanceWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void VideoButton_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                var videoAnimationWindow = new VideoAnimationWindow();
                videoAnimationWindow.Show();
            }
        }

        private void VideoButton2_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                var videoDrawingAnimationWindow = new VideoDrawingAnimationWindow();
                videoDrawingAnimationWindow.Show();
            }
        }

        private void GifButton_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Count; i++)
            {
                var gifAnimationWindow = new GifAnimationWindow();
                gifAnimationWindow.Show();
            }
        }

        private void NativeButton_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Count; i++)
            {
                var newCallAnimationWindow = new NewCallAnimationWindow();
                newCallAnimationWindow.Show();
            }
        }
    }
}
