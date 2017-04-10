using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfItemsControlSplitter
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> _source = new ObservableCollection<string>();

        /// <summary>
        /// 数据源
        /// </summary>
        public ObservableCollection<string> Source
        {
            get { return _source; }
            set { _source = value; }
        }

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
    }
}
