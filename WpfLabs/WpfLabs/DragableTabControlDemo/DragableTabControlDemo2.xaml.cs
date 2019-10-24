using System.Windows;

namespace WpfLabs.DragableTabControlDemo
{
    /// <summary>
    /// DragableTabControlDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DragableTabControlDemo2
    {
        public DragableTabControlDemo2()
        {
            InitializeComponent();

            DataContext = DragDropViewModel.Instance;
        }
    }
}