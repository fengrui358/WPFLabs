using System.Windows;

namespace WpfLabs.DragableTabControlDemo
{
    /// <summary>
    /// DragableTabControlDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DragableTabControlDemo
    {
        public DragableTabControlDemo()
        {
            InitializeComponent();

            DataContext = DragDropViewModel.Instance;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dragableTabControlDemo = new DragableTabControlDemo2();
            dragableTabControlDemo.Show();
        }
    }
}