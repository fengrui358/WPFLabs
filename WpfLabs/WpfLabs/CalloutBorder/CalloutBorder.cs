using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfLabs.CalloutBorder
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.CalloutBorder"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.CalloutBorder;assembly=WpfLabs.CalloutBorder"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CalloutBorder/>
    ///
    /// </summary>
    public class CalloutBorder : ContentControl
    {
        public enum EnumPlacement
        {
            /// <summary>
            /// 左
            /// </summary>
            Left,
            /// <summary>
            /// 右
            /// </summary>
            Right,
            /// <summary>
            /// 上
            /// </summary>
            Top,
            /// <summary>
            /// 下
            /// </summary>
            Bottom
        }

        static CalloutBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalloutBorder), new FrameworkPropertyMetadata(typeof(CalloutBorder)));
        }

        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register("Placement", typeof(EnumPlacement), typeof(CalloutBorder),
                new FrameworkPropertyMetadata(EnumPlacement.Left, FrameworkPropertyMetadataOptions.AffectsRender, OnDirectionPropertyChangedCallback));

        public EnumPlacement Placement
        {
            get { return (EnumPlacement)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public static readonly DependencyProperty TailWidthProperty =
            DependencyProperty.Register("TailWidth", typeof(double), typeof(CalloutBorder), new PropertyMetadata(10d));

        /// <summary>
        /// 尾巴的宽度，默认值为7
        /// </summary>
        public double TailWidth
        {
            get { return (double)GetValue(TailWidthProperty); }
            set { SetValue(TailWidthProperty, value); }
        }
        public static readonly DependencyProperty TailHeightProperty =
            DependencyProperty.Register("TailHeight", typeof(double), typeof(CalloutBorder), new PropertyMetadata(10d));

        /// <summary>
        /// 尾巴的高度，默认值为10
        /// </summary>
        public double TailHeight
        {
            get { return (double)GetValue(TailHeightProperty); }
            set { SetValue(TailHeightProperty, value); }
        }

        public static readonly DependencyProperty TailVerticalOffsetProperty =
            DependencyProperty.Register("TailVerticalOffset", typeof(double), typeof(CalloutBorder), new PropertyMetadata(13d));

        /// <summary>
        /// 尾巴距离顶部的距离，默认值为10
        /// </summary>
        public double TailVerticalOffset
        {
            get { return (double)GetValue(TailVerticalOffsetProperty); }
            set { SetValue(TailVerticalOffsetProperty, value); }
        }
        public static readonly DependencyProperty TailHorizontalOffsetProperty =
            DependencyProperty.Register("TailHorizontalOffset", typeof(double), typeof(CalloutBorder),
                new PropertyMetadata(12d));
        /// <summary>
        /// 尾巴距离顶部的距离，默认值为10
        /// </summary>
        public double TailHorizontalOffset
        {
            get { return (double)GetValue(TailHorizontalOffsetProperty); }
            set { SetValue(TailHorizontalOffsetProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius)
                , typeof(CalloutBorder), new PropertyMetadata(new CornerRadius(0)));
        /// <summary>
        /// 边框大小
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// 根据三角形方向设置消息框的水平位置，偏左还是偏右
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnDirectionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var self = d as CornerRadius;
            //self.HorizontalAlignment = ((EnumPlacement)e.NewValue == EnumPlacement.RightCenter) ?
            //    HorizontalAlignment.Right : HorizontalAlignment.Left;
        }
    }
}
