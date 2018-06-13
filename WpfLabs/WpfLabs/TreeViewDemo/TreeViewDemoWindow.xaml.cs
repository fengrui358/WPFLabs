using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfLabs.Annotations;

namespace WpfLabs.TreeViewDemo
{
    /// <summary>
    /// TreeViewDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TreeViewDemoWindow : Window, INotifyPropertyChanged
    {
        public List<TreeViewDemoItem> TreeViewItems { get; set; }

        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public TreeViewDemoWindow()
        {
            InitializeComponent();

            var flagTreeViewItem = new TreeViewDemoItem();

            TreeViewItems = new List<TreeViewDemoItem>();
            for (int i = 0; i < 5; i++)
            {
                var newTreeItem = new TreeViewDemoItem();
                for (int j = 0; j < 15; j++)
                {
                    var subTreeViewItem = new TreeViewDemoItem();
                    for (int k = 0; k < 25; k++)
                    {
                        if (i == 3 && j == 5 && k == 12)
                        {
                            subTreeViewItem.Children.Add(flagTreeViewItem);
                        }
                        else
                        {
                            subTreeViewItem.Children.Add(new TreeViewDemoItem());
                        }
                    }

                    newTreeItem.Children.Add(subTreeViewItem);
                }

                TreeViewItems.Add(newTreeItem);
            }

            DataContext = this;

            SelectedItem = flagTreeViewItem;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
