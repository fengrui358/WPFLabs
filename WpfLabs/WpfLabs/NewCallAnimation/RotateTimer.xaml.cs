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
        private DispatcherTimer _dispatcherTimer;

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(RotateTimer),
            new FrameworkPropertyMetadata(default(bool), OnIsActiveChanged));

        public static readonly DependencyProperty TotalTimeProperty = DependencyProperty.Register(
            "TotalTime", typeof(TimeSpan), typeof(RotateTimer), new PropertyMetadata(default(TimeSpan)));

        /// <summary>
        /// 总时间
        /// </summary>
        public TimeSpan TotalTime
        {
            get { return (TimeSpan) GetValue(TotalTimeProperty); }
            private set { SetValue(TotalTimeProperty, value); }
        }

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
                    rotateTimer.TotalTime = new TimeSpan();

                    rotateTimer._dispatcherTimer = new DispatcherTimer(TimeSpan.FromSeconds(1),
                        DispatcherPriority.Normal,
                        (sender, args) =>
                        {
                            rotateTimer.TotalTime = rotateTimer.TotalTime.Add(TimeSpan.FromSeconds(1));
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
    }
}