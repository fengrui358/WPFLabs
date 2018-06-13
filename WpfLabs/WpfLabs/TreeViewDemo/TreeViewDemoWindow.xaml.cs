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

namespace WpfLabs.TreeViewDemo
{
    /// <summary>
    /// TreeViewDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TreeViewDemoWindow : Window
    {
        public List<TreeViewDemoItem> TreeViewItems { get; set; }

        public TreeViewDemoWindow()
        {
            InitializeComponent();

            TreeViewItems = new List<TreeViewDemoItem>();
            for (int i = 0; i < 5; i++)
            {
                var newTreeItem = new TreeViewDemoItem
                {
                    Name = Guid.NewGuid().ToString("N"),
                    Children = new List<TreeViewDemoItem>()
                };
            }
        }
    }
}
