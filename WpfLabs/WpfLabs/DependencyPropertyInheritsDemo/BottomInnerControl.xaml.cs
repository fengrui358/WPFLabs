using System.Windows;
using System.Windows.Controls;

namespace WpfLabs.DependencyPropertyInheritsDemo
{
    /// <summary>
    /// BottomInnerControl.xaml 的交互逻辑
    /// </summary>
    public partial class BottomInnerControl : UserControl
    {
        public static readonly DependencyProperty TestStringProperty = DependencyProperty.Register(
            "TestString", typeof(string), typeof(BottomInnerControl), new PropertyMetadata(default(string)));

        public string TestString
        {
            get => (string) GetValue(TestStringProperty);
            set => SetValue(TestStringProperty, value);
        }

        public BottomInnerControl()
        {
            InitializeComponent();
        }
    }
}
