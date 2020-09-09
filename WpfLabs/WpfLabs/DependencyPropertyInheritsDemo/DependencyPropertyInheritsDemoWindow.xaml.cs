using System.Windows;

namespace WpfLabs.DependencyPropertyInheritsDemo
{
    /// <summary>
    /// DependencyPropertyInheritsDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DependencyPropertyInheritsDemoWindow : Window
    {
        public TargetDependencyProperty Target { get; set; }

        public DependencyPropertyInheritsDemoWindow()
        {
            InitializeComponent();

            Target = new TargetDependencyProperty
            {
                Test = "测试依赖属性传递"
            };

            DataContext = this;
        }
    }
}
