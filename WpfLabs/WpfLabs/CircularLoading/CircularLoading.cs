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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLabs.CircularLoading
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.CircularLoading"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.CircularLoading;assembly=WpfLabs.CircularLoading"
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
    ///     <MyNamespace:CircularLoading/>
    ///
    /// </summary>
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class CircularLoading : Control
    {
        private List<Action> _deferredActions = new List<Action>();

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(CircularLoading), new PropertyMetadata(false, IsActiveChanged));

        public static readonly DependencyProperty ShortSegmentBrushProperty = DependencyProperty.Register(
            "ShortSegmentBrush", typeof(Brush), typeof(CircularLoading), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.White), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush ShortSegmentBrush
        {
            get { return (Brush) GetValue(ShortSegmentBrushProperty); }
            set { SetValue(ShortSegmentBrushProperty, value); }
        }

        public static readonly DependencyProperty LongSegmentBrushProperty = DependencyProperty.Register(
            "LongSegmentBrush", typeof(Brush), typeof(CircularLoading),
            new FrameworkPropertyMetadata(new SolidColorBrush(new Color {R = 0x3C, G = 0xD9, B = 0xD5}), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Brush LongSegmentBrush
        {
            get { return (Brush) GetValue(LongSegmentBrushProperty); }
            set { SetValue(LongSegmentBrushProperty, value); }
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        static CircularLoading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircularLoading), new FrameworkPropertyMetadata(typeof(CircularLoading)));
        }

        private static void IsActiveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var circularLoading = dependencyObject as CircularLoading;
            if (circularLoading == null)
                return;

            circularLoading.UpdateActiveState();
        }

        private void UpdateActiveState()
        {
            Action action;

            if (IsActive)
                action = () => VisualStateManager.GoToState(this, "Active", true);
            else
                action = () => VisualStateManager.GoToState(this, "Inactive", true);

            if (_deferredActions != null)
                _deferredActions.Add(action);

            else
                action();
        }

        public override void OnApplyTemplate()
        {
            //make sure the states get updated
            UpdateActiveState();
            base.OnApplyTemplate();
            if (_deferredActions != null)
                foreach (var action in _deferredActions)
                    action();
            _deferredActions = null;
        }
    }
}
