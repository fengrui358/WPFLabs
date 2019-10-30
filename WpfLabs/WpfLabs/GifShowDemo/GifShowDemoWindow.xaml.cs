using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
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

            ImageBehavior.SetRepeatBehavior(GifImage, new RepeatBehavior(1));
        }

        private void ImageBehavior_OnAnimationCompleted(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Gif animation completed.");
            //ImageBehavior.SetAnimatedSource(GifImage2, new BitmapImage(new Uri("pack://application:,,,/WpfLabs;component/GifShowDemo/Test.gif")));
            GifImage.Visibility = Visibility.Collapsed;
            GifImage2.Visibility = Visibility.Visible;

            ImageBehavior.SetAnimatedSource(GifImage, null);
            

            ((Grid) GifImage.Parent).Children.Remove(GifImage);
        }
    }
}
