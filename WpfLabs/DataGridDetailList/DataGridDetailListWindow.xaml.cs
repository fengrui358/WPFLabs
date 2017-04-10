using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfLabs.DataGridDetailList
{
    /// <summary>
    /// DataGridDetailListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridDetailListWindow : Window
    {
        public List<string> TestItemSource { get; set; }

        public List<string> TestItemSource2 { get; set; }

        public DataGridDetailListWindow()
        {
            InitializeComponent();

            TestItemSource = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                TestItemSource.Add(i.ToString());
            }

            TestItemSource2 = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                TestItemSource2.Add(Guid.NewGuid().ToString());
            }

            DataContext = this;
        }
    }
}
