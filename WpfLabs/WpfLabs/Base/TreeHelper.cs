using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace WpfLabs.Base
{
    /// <summary>
    /// 逻辑树或视觉树辅助类。
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// 查找祖先节点上某个指定的节点。
        /// </summary>
        /// <typeparam name="T">要查找的节点类型。</typeparam>
        /// <param name="current">开始查找的当前节点。</param>
        /// <returns></returns>
        public static T FindAncestorNode<T>(DependencyObject current) where T : DependencyObject
        {
            if (current == null)
                return null;

            if (current is T)
                return (T) current;

            DependencyObject parent = GetParent(current);
            while (parent != null)
            {
                return parent is T ? parent as T : FindAncestorNode<T>(parent);
            }
            return null;
        }

        private static DependencyObject GetParent(DependencyObject dpObj)
        {
            if (dpObj == null)
                return null;

            return dpObj is Visual || dpObj is Visual3D
                ? VisualTreeHelper.GetParent(dpObj)
                : LogicalTreeHelper.GetParent(dpObj);
        }

        /// <summary>
        /// 获取父可视对象中第一个指定类型的子可视对象
        /// </summary>
        /// <typeparam name="T">可视对象类型</typeparam>
        /// <param name="parent">父可视对象</param>
        /// <param name="Tag">可视化对象上的Tag标记</param>
        /// <returns>第一个指定类型的子可视对象</returns>
        public static T GetVisualChild<T>(Visual parent, string Tag = "") where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual) VisualTreeHelper.GetChild(parent, i);

                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    //判断Tag标签
                    if (!string.IsNullOrEmpty(Tag))
                    {
                        var tagFrameworkElement = child as FrameworkElement;
                        if (tagFrameworkElement != null && tagFrameworkElement.Tag != null &&
                            tagFrameworkElement.Tag.ToString() == Tag)
                        {
                            break;
                        }
                        else
                        {
                            child = GetVisualChild<T>(v, Tag);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return child;
        }
    }
}