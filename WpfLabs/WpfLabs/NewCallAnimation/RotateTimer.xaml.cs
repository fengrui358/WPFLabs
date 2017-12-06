using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfLabs.NewCallAnimation
{
    /// <summary>
    /// RotateTimer.xaml 的交互逻辑
    /// </summary>
    public partial class RotateTimer
    {
        private TimeSpan _timeSpan = new TimeSpan();
        private DispatcherTimer _dispatcherTimer;

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(RotateTimer),
            new FrameworkPropertyMetadata(default(bool), OnIsActiveChanged));

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public RotateTimer()
        {
            InitializeComponent();
        }

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rotateTimer = (RotateTimer) d;
            var storyBoard = (Storyboard) rotateTimer.Resources["Rotate"];

            if ((bool) e.NewValue)
            {
                storyBoard.Begin();
                if (rotateTimer._dispatcherTimer == null)
                {
                    rotateTimer._timeSpan = new TimeSpan();
                    rotateTimer.ShowTime();

                    rotateTimer._dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1),
                        DispatcherPriority.Normal,
                        (sender, args) =>
                        {
                            rotateTimer._timeSpan = rotateTimer._timeSpan.Add(TimeSpan.FromSeconds(1));
                            rotateTimer.ShowTime();
                        },
                        rotateTimer.Dispatcher);
                }
            }
            else
            {
                storyBoard.Stop();
                if (rotateTimer._dispatcherTimer != null)
                {
                    rotateTimer._dispatcherTimer.Stop();
                    rotateTimer._dispatcherTimer = null;
                }
            }
        }

        private void ShowTime()
        {
            if (_timeSpan.Hours > 0)
            {
                Time.Text = string.Format("{0}:{1}:{2}", _timeSpan.Hours.ToString().PadLeft(2, '0'),
                    _timeSpan.Minutes.ToString().PadLeft(2, '0'), _timeSpan.Seconds.ToString().PadLeft(2, '0'));
            }
            else
            {
                Time.Text = string.Format("{0}:{1}", _timeSpan.Minutes.ToString().PadLeft(2, '0'),
                    _timeSpan.Seconds.ToString().PadLeft(2, '0'));
            }
        }
    }
}