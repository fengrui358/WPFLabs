using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfLabs.LoadingControl
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.LoadingControl"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.LoadingControl;assembly=WpfLabs.LoadingControl"
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
    ///     <MyNamespace:LoadingControl/>
    ///
    /// </summary>
    [TemplateVisualState(Name = "Inactive", GroupName = "ActiveStates")]
    [TemplateVisualState(Name = "Active", GroupName = "ActiveStates")]
    public class LoadingControl : Control
    {
        private List<Action> _deferredActions;

        /// <summary>
        /// 获取或设置是否激活
        /// </summary>
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        /// <summary>
        /// 是否激活的DP属性
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive",
            typeof(bool),
            typeof(LoadingControl),
            new PropertyMetadata(false, IsActiveChanged));

        /// <summary>
        /// 当是否激活属性变化时的处理方法
        /// </summary>
        /// <param name="dependencyObject">当前控件实例</param>
        /// <param name="dependencyPropertyChangedEventArgs">参数</param>
        private static void IsActiveChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var loadingControl = dependencyObject as LoadingControl;
            loadingControl?.UpdateActiveState();
        }

        public static readonly DependencyProperty LoadingMessageProperty = DependencyProperty.Register(
            "LoadingMessage", typeof(string), typeof(LoadingControl), new PropertyMetadata(default(string)));

        public string LoadingMessage
        {
            get => (string)GetValue(LoadingMessageProperty);
            set => SetValue(LoadingMessageProperty, value);
        }
        static LoadingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadingControl), new FrameworkPropertyMetadata(typeof(LoadingControl)));
        }

        public LoadingControl()
        {
            _deferredActions = new List<Action>();
        }

        /// <summary>
        /// 更新激活状态的方法
        /// </summary>
        private void UpdateActiveState()
        {
            Action action;

            if (IsActive)
            {
                action = () =>
                {
                    Visibility = Visibility.Visible;
                    VisualStateManager.GoToState(this, "Active", true);
                };
            }
            else
            {
                action = () =>
                {
                    Visibility = Visibility.Collapsed;
                    VisualStateManager.GoToState(this, "Inactive", true);
                };
            }

            if (_deferredActions != null)
            {
                _deferredActions.Clear();
                _deferredActions.Add(action);
            }
            else
            {
                action();
            }
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
