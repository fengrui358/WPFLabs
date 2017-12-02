using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using EasingFunctionDemo.EasingFunctionConfigs;

namespace EasingFunctionDemo
{
    /// <summary>
    /// AnimationPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationPanel
    {
        public DoubleAnimation DoubleAnimation { get; set; }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            "EasingFunction", typeof(EasingFunctionConfig), typeof(AnimationPanel),
            new FrameworkPropertyMetadata(EasingFunctionPropertyChanged));

        public EasingFunctionConfig EasingFunction
        {
            get => (EasingFunctionConfig) GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
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
            DoubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 240,
                Duration = TimeSpan.FromSeconds(4),
                RepeatBehavior = RepeatBehavior.Forever,
                EasingFunction = EasingFunction?.ConfigEasingFunction
            };
            Rec.BeginAnimation(Canvas.LeftProperty, DoubleAnimation);
        }
    }
}