using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace WpfItemsControlSeparator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public ObservableCollection<string> Source { get; set; } = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 3; i++)
            {
                Source.Add(Guid.NewGuid().ToString("N"));
            }

            DataContext = this;
        }

        private void InsertBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Source.Insert(0, Guid.NewGuid().ToString("N"));
        }

        private void AppendBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Source.Add(Guid.NewGuid().ToString("N"));
        }

        private void DeleteFirst_OnClick(object sender, RoutedEventArgs e)
        {
            if (Source.Any())
            {
                Source.RemoveAt(0);
            }
        }

        private void DeleteLast_OnClick(object sender, RoutedEventArgs e)
        {
            if (Source.Any())
            {
                Source.RemoveAt(Source.Count - 1);
            }
        }
    }
}