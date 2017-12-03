using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using EasingFunctionDemo.EasingFunctionConfigs;

namespace EasingFunctionDemo
{
    /// <summary>
    /// AnimationPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationPanel
    {
        private const int TotalMilliSeconds = 4 * 1000;

        public Storyboard Storyboard { get; set; }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            "EasingFunction", typeof(EasingFunctionConfig), typeof(AnimationPanel),
            new FrameworkPropertyMetadata(EasingFunctionPropertyChanged));

        public EasingFunctionConfig EasingFunction
        {
            get => (EasingFunctionConfig) GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public static readonly DependencyProperty CurrentProgressProperty = DependencyProperty.Register(
            "CurrentProgress", typeof(double), typeof(AnimationPanel), new PropertyMetadata(default(double)));

        public double CurrentProgress
        {
            get => (double) GetValue(CurrentProgressProperty);
            private set => SetValue(CurrentProgressProperty, value);
        }

        public AnimationPanel()
        {
            InitializeComponent();
        }

        private static void EasingFunctionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animationPanel = (AnimationPanel) d;
            var oldEasingFunctionConfig = e.OldValue as EasingFunctionConfig;
            var newEasingFunctionConfig = e.NewValue as EasingFunctionConfig;

            if (oldEasingFunctionConfig != null)
            {
                oldEasingFunctionConfig.ConfigEasingFunctionChanged -= animationPanel.OnEasingFunctionConfigChanged;
            }

            if (newEasingFunctionConfig != null)
            {
                newEasingFunctionConfig.ConfigEasingFunctionChanged += animationPanel.OnEasingFunctionConfigChanged;
            }

            animationPanel.SetNewAnimation();
        }

        private void OnEasingFunctionConfigChanged(object sender, EventArgs args)
        {
            SetNewAnimation();
        }

        private void SetNewAnimation()
        {
            if(Storyboard == null)
            {
                Storyboard = new Storyboard();
                Storyboard.CurrentTimeInvalidated += StoryboardOnCurrentTimeInvalidated;
            }

            Storyboard.Stop();
            Storyboard.Children.Clear();

            var doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 240,
                Duration = TimeSpan.FromSeconds(4),
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = EasingFunction?.ConfigEasingFunction
            };
            Storyboard.SetTargetName(doubleAnimation, nameof(Rec));
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Canvas.LeftProperty));
            Storyboard.Children.Add(doubleAnimation);

            var colorAnimation = new ColorAnimation()
            {
                From = Colors.Red,
                To = Colors.Blue,
                Duration = TimeSpan.FromSeconds(4),
                RepeatBehavior = RepeatBehavior.Forever
            };
            Storyboard.SetTargetName(colorAnimation, nameof(Rec));
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(Shape.Fill).(SolidColorBrush.Color)"));
            Storyboard.Children.Add(colorAnimation);

            Storyboard.Begin(Rec);
        }

        private void StoryboardOnCurrentTimeInvalidated(object sender, EventArgs eventArgs)
        {
            var clock = (Clock) sender;
            if (clock?.CurrentTime != null)
            {
                var cycleMilliseconds = clock.CurrentTime.Value.TotalMilliseconds - ConstData.TotalMilliSeconds *
                        Math.Floor(clock.CurrentTime.Value.TotalMilliseconds / ConstData.TotalMilliSeconds);

                CurrentProgress = cycleMilliseconds;
            }
        }
    }
}