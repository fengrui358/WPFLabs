using System.Collections.ObjectModel;
using System.Windows;

namespace WpfLabs.DragableListBoxDemo
{
    /// <summary>
    /// DragableListBoxDemo.xaml 的交互逻辑
    /// </summary>
    public partial class DragableListBoxDemo2 : Window
    {
        public ObservableCollection<MenuUiModel> Menus { get; set; }

        public int OrderIndex { get; } = 2;

        public DragableListBoxDemo2()
        {
            InitializeComponent();

            Menus = new ObservableCollection<MenuUiModel>();
            DataContext = this;
        }

        private void DragableListBoxDemo_OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                Menus.Add(new MenuUiModel{Header = $"Header_{i + 1}"});
            }
        }
    }
}
