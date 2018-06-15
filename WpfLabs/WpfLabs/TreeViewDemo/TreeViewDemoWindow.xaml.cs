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

        public Func<object, IEnumerable<object>> GetAllParentsFun { get; set; }

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

            GetAllParentsFun = GetAllParentsFunHandler;

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

        private IEnumerable<object> GetAllParentsFunHandler(object arg)
        {
            var result = new List<TreeViewDemoItem>();

            if (arg is TreeViewDemoItem treeViewDemoItem)
            {
                foreach (var treeViewItem in TreeViewItems)
                {
                    result = GetParentNodes(treeViewItem, treeViewDemoItem);
                    if (result != null)
                    {
                        result.Insert(0, treeViewItem);

                        //移除最后一个自身元素
                        result.RemoveAt(result.Count - 1);
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 查找父路径
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns>父节点</returns>
        private List<TreeViewDemoItem> GetParentNodes(TreeViewDemoItem source, TreeViewDemoItem target)
        {
            if (target == null || source == null)
            {
                return null;
            }

            if (source == target)
            {
                //用非null的空集合表示匹配成功
                return new List<TreeViewDemoItem>();
            }
            else
            {
                var result = new List<TreeViewDemoItem>();

                foreach (var treeViewDemoItem in source.Children)
                {
                    var getResult = GetParentNodes(treeViewDemoItem, target);
                    if (getResult != null)
                    {
                        result.Add(treeViewDemoItem);

                        result.AddRange(getResult);
                        return result;
                    }
                }
            }

            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
