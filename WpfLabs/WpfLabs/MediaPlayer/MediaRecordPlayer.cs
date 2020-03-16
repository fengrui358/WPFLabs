using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfLabs.MediaPlayer
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.MediaPlayer"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.MediaPlayer;assembly=WpfLabs.MediaPlayer"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:MediaPlayer/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_PlayButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_StopButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_PauseButton", Type = typeof(Button))]
    public class MediaRecordPlayer : Control
    {
        private DispatcherTimer _timer;
        private Button _playButton;
        private Button _stopBtn;
        private Button _pauseBtn;

        private System.Windows.Media.MediaPlayer _mediaPlayer;
        private PlayingStatus _currentPlayingStatus = PlayingStatus.Stop;

        public static readonly DependencyProperty UrlProperty = DependencyProperty.Register(
            "Url", typeof(string), typeof(MediaRecordPlayer), new PropertyMetadata(default(string), UrlPropertyChangedCallback));

        public string Url
        {
            get => (string) GetValue(UrlProperty);
            set => SetValue(UrlProperty, value);
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            "IsPlaying", typeof(bool), typeof(MediaRecordPlayer), new PropertyMetadata(default(bool), IsPlayingPropertyChangedCallback));

        public bool IsPlaying
        {
            get => (bool) GetValue(IsPlayingProperty);
            set => SetValue(IsPlayingProperty, value);
        }

        static MediaRecordPlayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MediaRecordPlayer), new FrameworkPropertyMetadata(typeof(MediaRecordPlayer)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _playButton = (Button) GetTemplateChild("PART_PlayButton");
            if (_playButton != null) _playButton.Click += (sender, args) => IsPlaying = true;

            _stopBtn = (Button)GetTemplateChild("PART_StopButton");
            if (_stopBtn != null) _stopBtn.Click += (sender, args) => IsPlaying = false;

            _pauseBtn = (Button)GetTemplateChild("PART_PauseButton");
            if (_pauseBtn != null) _pauseBtn.Click += (sender, args) => IsPlaying = false;
        }

        private static void UrlPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mediaRecordPlayer = (MediaRecordPlayer) d;

            mediaRecordPlayer._mediaPlayer?.Stop();
        }

        /// <summary>
        /// 是否正在播放属性改变
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void IsPlayingPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mediaRecordPlayer = (MediaRecordPlayer)d;

            if (mediaRecordPlayer.IsPlaying)
            {
                if (string.IsNullOrEmpty(mediaRecordPlayer.Url))
                {
                    mediaRecordPlayer.IsPlaying = false;
                }
                else
                {
                    mediaRecordPlayer.IsPlaying = true;
                    mediaRecordPlayer.ChangePlayingStatus(PlayingStatus.Playing);
                }
            }
            else
            {
                mediaRecordPlayer.ChangePlayingStatus(mediaRecordPlayer._currentPlayingStatus == PlayingStatus.Playing
                    ? PlayingStatus.Pause
                    : PlayingStatus.Stop);
            }
        }

        private void ChangePlayingStatus(PlayingStatus playingStatus)
        {
            if (_currentPlayingStatus != playingStatus)
            {
                _currentPlayingStatus = playingStatus;

                if (_currentPlayingStatus == PlayingStatus.Playing)
                {
                    //判断Url是否存在
                    if (string.IsNullOrEmpty(Url))
                    {
                        IsPlaying = false;
                    }

                    if (_mediaPlayer == null)
                    {
                        InitInnerMediaPlayer();
                        _timer = new DispatcherTimer(DispatcherPriority.SystemIdle, Dispatcher.CurrentDispatcher)
                        {
                            Interval = TimeSpan.FromMilliseconds(400)
                        };
                        _timer.Tick += TimerOnTick;
                    }

                    //判断是否从暂停状态恢复
                    if (_mediaPlayer.Position != TimeSpan.Zero)
                    {
                        _mediaPlayer.Play();
                        _timer.Start();
                    }
                    else
                    {
                        _mediaPlayer.Open(new Uri(Url, UriKind.Absolute));
                        _timer.Start();
                    }
                }
                else if(_currentPlayingStatus == PlayingStatus.Pause)
                {
                    _timer?.Stop();
                    _mediaPlayer?.Pause();
                }
                else if(_currentPlayingStatus == PlayingStatus.Stop)
                {
                    _timer?.Stop();
                    _mediaPlayer?.Stop();
                    _mediaPlayer?.Close();

                    //todo:清空时间
                }

                switch (_currentPlayingStatus)
                {
                    case PlayingStatus.Playing:
                        if (_playButton != null) _playButton.Visibility = Visibility.Hidden;
                        if (_stopBtn != null) _stopBtn.Visibility = Visibility.Hidden;
                        if (_pauseBtn != null) _pauseBtn.Visibility = Visibility.Visible;
                        break;
                    case PlayingStatus.Stop:
                    case PlayingStatus.Pause:
                        if (_playButton != null) _playButton.Visibility = Visibility.Visible;
                        if (_stopBtn != null) _stopBtn.Visibility = Visibility.Hidden;
                        if (_pauseBtn != null) _pauseBtn.Visibility = Visibility.Hidden;
                        break;
                }
            }
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            if (_mediaPlayer != null)
            {
                var total = _mediaPlayer.NaturalDuration;
                var value = _mediaPlayer.Position;

                Debug.WriteLine($"{value}/{total}");
            }
        }

        /// <summary>
        /// 尝试初始化内部播放器
        /// </summary>
        private void InitInnerMediaPlayer()
        {
            _mediaPlayer = new System.Windows.Media.MediaPlayer();
            _mediaPlayer.MediaFailed += (sender, args) => { IsPlaying = false; };
            _mediaPlayer.MediaOpened += (sender, args) =>
            {
                _mediaPlayer.Play();
            };
            _mediaPlayer.MediaEnded += (sender, args) => { IsPlaying = false; };
        }

        enum PlayingStatus
        {
            /// <summary>
            /// 停止
            /// </summary>
            Stop,

            /// <summary>
            /// 正在播放
            /// </summary>
            Playing,

            /// <summary>
            /// 暂停
            /// </summary>
            Pause
        }
    }
}
