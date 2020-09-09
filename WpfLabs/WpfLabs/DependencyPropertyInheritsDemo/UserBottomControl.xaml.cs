using System.Windows;

namespace WpfLabs.DependencyPropertyInheritsDemo
{
    /// <summary>
    /// UserBottomControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserBottomControl
    {
        public static readonly DependencyProperty TargetProperty = DependencyProperties.TargetProperty.AddOwner(typeof(UserBottomControl));

        public TargetDependencyProperty Target
        {
            get => (TargetDependencyProperty)GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        public UserBottomControl()
        {
            InitializeComponent();
        }
    }
}
