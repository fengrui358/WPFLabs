using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLabs.MusicPlayer
{
    /// <summary>
    /// MusicPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class MusicPlayer : UserControl
    {
        public static readonly DependencyProperty SoundResourceProperty = DependencyProperty.Register(
            "SoundResource", typeof(string), typeof(MusicPlayer),
            new PropertyMetadata(default(string), ResourceChangedCallback));

        /// <summary>
        /// 待播放的文件路径，可以是网络路径，也可以是本地路径
        /// </summary>
        public string SoundResource
        {
            get { return (string) GetValue(SoundResourceProperty); }
            set { SetValue(SoundResourceProperty, value); }
        }

        public static readonly DependencyProperty AutoPlayProperty = DependencyProperty.Register(
            "AutoPlay", typeof(bool), typeof(MusicPlayer), new PropertyMetadata(true, ResourceChangedCallback));



        /// <summary>
        /// 自动播放，当音频文件准备好后立即自动播放
        /// </summary>
        public bool AutoPlay
        {
            get { return (bool) GetValue(AutoPlayProperty); }
            set { SetValue(AutoPlayProperty, value); }
        }

        public MusicPlayer()
        {
            InitializeComponent();

            NAudioEngine.Instance.PropertyChanged += NAudioEngine_PropertyChanged;
            SpectrumAnalyzer.RegisterSoundPlayer(NAudioEngine.Instance);
        }

        private void NAudioEngine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ChannelPosition":
                    //clockDisplay.Time = TimeSpan.FromSeconds(engine.ChannelPosition);
                    break;
                case "ChannelLength":

                default:
                    // Do Nothing
                    break;
            }
        }

        private static void ResourceChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var player = (MusicPlayer) dependencyObject;

            if (dependencyPropertyChangedEventArgs.NewValue != null)
            {
                //todo:各种校验
                var filePath = dependencyPropertyChangedEventArgs.NewValue.ToString();

                NAudioEngine.Instance.OpenFile(dependencyPropertyChangedEventArgs.NewValue.ToString());
                //NAudioEngine.Instance.OpenFile(filePath);

                if (player.AutoPlay && NAudioEngine.Instance.CanPlay)
                {
                    NAudioEngine.Instance.Play();
                }
            }
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }
    }
}