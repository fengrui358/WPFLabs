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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasingFunctionDemo
{
    /// <summary>
    /// AnimationPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationPanel : UserControl
    {
        public DoubleAnimation DoubleAnimation { get; set; }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            "EasingFunction", typeof(IEasingFunction), typeof(AnimationPanel), new FrameworkPropertyMetadata(EasingFunctionPropertyChanged));

        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction) GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        public AnimationPanel()
        {
            InitializeComponent();

            DoubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 240,
                Duration = TimeSpan.FromSeconds(4),
                RepeatBehavior = RepeatBehavior.Forever
            };

            Rec.BeginAnimation(Canvas.LeftProperty, DoubleAnimation);
        }

        private static void EasingFunctionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var animationPanel = (AnimationPanel) d;
            animationPanel.DoubleAnimation.EasingFunction = (IEasingFunction) e.NewValue;
        }
    }
}
