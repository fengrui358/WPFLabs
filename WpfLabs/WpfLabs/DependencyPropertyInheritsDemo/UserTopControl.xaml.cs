using System.Windows;
using System.Windows.Controls;

namespace WpfLabs.DependencyPropertyInheritsDemo
{
    /// <summary>
    /// UserTopControl.xaml 的交互逻辑
    /// </summary>
    public partial class UserTopControl
    {
        public static readonly DependencyProperty TargetProperty = DependencyProperties.TargetProperty.AddOwner(typeof(UserTopControl));

        public TargetDependencyProperty Target
        {
            get => (TargetDependencyProperty) GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        public UserTopControl()
        {
            InitializeComponent();
        }
    }
}
