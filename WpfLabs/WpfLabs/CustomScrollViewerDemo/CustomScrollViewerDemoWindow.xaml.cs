using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfLabs.CustomScrollViewerDemo
{
    /// <summary>
    /// CustomScrollViewerDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CustomScrollViewerDemoWindow : Window
    {
        private Random _random = new Random();

        public ObservableCollection<string> Items { get; set; }

        public CustomScrollViewerDemoWindow()
        {
            InitializeComponent();
            DataContext = this;

            Items = new ObservableCollection<string>();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Items.Add(Guid.NewGuid().ToString().Substring(_random.Next(3, 8), _random.Next(3, 7)));
        }
    }
}
