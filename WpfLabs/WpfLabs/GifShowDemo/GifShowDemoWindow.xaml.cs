using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Animation;
using WpfAnimatedGif;

namespace WpfLabs.GifShowDemo
{
    /// <summary>
    /// GifShowDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GifShowDemoWindow : Window
    {
        public GifShowDemoWindow()
        {
            InitializeComponent();

            //ImageBehavior.SetRepeatBehavior(GifImage, new RepeatBehavior(1));
        }

        private void ImageBehavior_OnAnimationCompleted(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Gif animation completed.");
        }
    }
}
