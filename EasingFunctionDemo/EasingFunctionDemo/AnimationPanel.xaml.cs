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
        public AnimationPanel()
        {
            InitializeComponent();

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 240;
            doubleAnimation.Duration = TimeSpan.FromSeconds(4);
            doubleAnimation.EasingFunction = new TestEasingFunction();

            Rec.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }
    }
}
