using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace WpfLabs.Behaviors
{
    public class TreeViewSelectionBehavior : Behavior<TreeView>
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(nameof(SelectedItem), typeof(object),
                typeof(TreeViewSelectionBehavior),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedItemChanged));

        public static readonly DependencyProperty GetAllParentsFunProperty =
            DependencyProperty.Register(nameof(GetAllParentsFun), typeof(Func<object, IEnumerable<object>>),
                typeof(TreeViewSelectionBehavior),
                new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ExpandSelectedProperty =
            DependencyProperty.Register(nameof(ExpandSelected), typeof(bool),
                typeof(TreeViewSelectionBehavior),
                new FrameworkPropertyMetadata(false));

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var behavior = (TreeViewSelectionBehavior) sender;
            if (behavior._modelHandled) return;

            if (behavior.AssociatedObject == null)
                return;

            behavior._modelHandled = true;
            behavior.UpdateAllTreeViewItems();
            behavior._modelHandled = false;
        }

        private bool _modelHandled;

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public Func<object, IEnumerable<object>> GetAllParentsFun
        {
            get => (Func<object, IEnumerable<object>>) GetValue(GetAllParentsFunProperty);
            set => SetValue(GetAllParentsFunProperty, value);
        }

        /// <summary>
        /// 自动展开选中项
        /// </summary>
        public bool ExpandSelected
        {
            get => (bool) GetValue(ExpandSelectedProperty);
            set => SetValue(ExpandSelectedProperty, value);
        }

        /// <summary>
        /// 遍历树，自动展开选中项
        /// </summary>
        private void UpdateAllTreeViewItems()
        {
            var treeView = AssociatedObject;

            if (SelectedItem == null)
            {
                //清空选中项
                if (treeView.ItemContainerGenerator.ContainerFromItem(treeView.SelectedItem) is TreeViewItem
                    selectedTreeViewItem)
                {//todo清空
                    selectedTreeViewItem.IsSelected = false;
                }
            }
            else
            {
                var isMatch = false;
                var allParents = GetAllParentsFun?.Invoke(SelectedItem)?.ToList();
                if (allParents != null && allParents.Any())
                {
                    //遍历节点与逻辑父节点进行匹配
                    foreach (var item in treeView.Items)
                    {
                        if (treeView.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem tvi)
                        {
                            if (!UpdateTreeViewItem(tvi, allParents, SelectedItem, null, out isMatch))
                            {
                                break;
                            }
                        }
                    }
                }

                //遍历所有可见的展开节点
                if (!isMatch)
                {
                    //遍历UI可见的所有节点
                    foreach (var item in treeView.Items)
                    {
                        if (treeView.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem tvi)
                        {
                            if (!UpdateTreeViewItem(tvi, SelectedItem, null))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有的节点路径
        /// </summary>
        /// <param name="treeViewItem">树节点</param>
        /// <param name="allParents">所有的父对象</param>
        /// <param name="selectedItem">选中节点</param>
        /// <param name="parenTreeViewItem">父级树节点</param>
        /// <param name="match">是否匹配中</param>
        /// <returns>是否继续</returns>
        private bool UpdateTreeViewItem(TreeViewItem treeViewItem, List<object> allParents, object selectedItem, TreeViewItem parenTreeViewItem, out bool match)
        {
            match = false;

            if (allParents == null || !allParents.Any())
            {
                return false;
            }

            var treeView = AssociatedObject;

            if (treeViewItem != null)
            {
                var parentItemContainerGenerator = parenTreeViewItem == null ? treeView.ItemContainerGenerator : parenTreeViewItem.ItemContainerGenerator;

                if (parentItemContainerGenerator
                        .ContainerFromItem(selectedItem) is TreeViewItem selectedTreeViewItem &&
                    Equals(selectedTreeViewItem, treeViewItem))
                {
                    //已经匹配成功
                    selectedTreeViewItem.IsSelected = true;
                    if (ExpandSelected && selectedTreeViewItem.HasItems && !selectedTreeViewItem.IsExpanded)
                    {
                        selectedTreeViewItem.IsExpanded = true;
                    }

                    match = true;
                    return false;
                }

                object matchObj = null;
                TreeViewItem matchTreeViewItem = null;

                foreach (var parent in allParents)
                {
                    if (Equals(parent, treeViewItem.DataContext))
                    {
                        matchObj = parent;
                        matchTreeViewItem = parentItemContainerGenerator
                            .ContainerFromItem(parent) as TreeViewItem;
                        if (matchTreeViewItem != null && !matchTreeViewItem.IsExpanded)
                        {
                            matchTreeViewItem.IsExpanded = true;
                        }

                        break;
                    }
                }

                if (matchObj != null && matchTreeViewItem != null)
                {
                    allParents.Remove(matchObj);

                    foreach (var item in matchTreeViewItem.Items)
                    {
                        if (matchTreeViewItem.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem tvi)
                        {
                            if (!UpdateTreeViewItem(tvi, allParents, SelectedItem, matchTreeViewItem, out match))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 遍历所有可见的节点
        /// </summary>
        /// <param name="item"></param>
        /// <param name="selectedItem"></param>
        /// <param name="parenTreeViewItem"></param>
        /// <param name="isSelected">匹配后是否选中</param>
        /// <returns>是否继续</returns>
        private bool UpdateTreeViewItem(TreeViewItem item, object selectedItem, TreeViewItem parenTreeViewItem, bool isSelected = true)
        {
            if (selectedItem == null) return true;

            var treeView = AssociatedObject;

            if (item != null)
            {
                var parent = parenTreeViewItem == null ? treeView.ItemContainerGenerator : parenTreeViewItem.ItemContainerGenerator;
                
                if (parent.ContainerFromItem(selectedItem) is TreeViewItem
                        selectedTreeViewItem && Equals(selectedTreeViewItem, item))
                {
                    if (isSelected)
                    {
                        selectedTreeViewItem.IsSelected = true;
                        if (ExpandSelected && selectedTreeViewItem.HasItems && !selectedTreeViewItem.IsExpanded)
                        {
                            selectedTreeViewItem.IsExpanded = true;
                        }
                    }
                    else
                    {
                        selectedTreeViewItem.IsSelected = false;
                    }

                    return false;
                }

                foreach (var subItem in item.Items)
                {
                    if (item.ItemContainerGenerator.ContainerFromItem(subItem) is TreeViewItem tvi)
                    {
                        if (!UpdateTreeViewItem(tvi, SelectedItem, item))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// UI选中项变更，通知Model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> args)
        {
            if (_modelHandled) return;

            SelectedItem = args.NewValue;
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;

            if (AssociatedObject.SelectedItem != null || SelectedItem != null)
            {
                _modelHandled = true;
                UpdateAllTreeViewItems();
                _modelHandled = false;
            }
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.SelectedItem != null || SelectedItem != null)
            {
                _modelHandled = true;
                UpdateAllTreeViewItems();
                _modelHandled = false;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null)
            {
                AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
                AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
            }
        }
    }
}