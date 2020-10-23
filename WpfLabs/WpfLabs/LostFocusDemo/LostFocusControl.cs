using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLabs.LostFocusDemo
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.LostFocusDemo"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.LostFocusDemo;assembly=WpfLabs.LostFocusDemo"
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
    ///     <MyNamespace:LostFocusControl/>
    ///
    /// </summary>
    public class LostFocusControl : Control
    {
        private TextBlock _msgTextBlock;
        private ToggleButton _toggleButton;

        static LostFocusControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LostFocusControl), new FrameworkPropertyMetadata(typeof(LostFocusControl)));
        }

        public LostFocusControl()
        {
            LostFocus += (sender, args) =>
            {
                _msgTextBlock.Text = $"{_msgTextBlock.Text}{Environment.NewLine}LostFocus.";
                _toggleButton.IsChecked = false;
            };

            GotFocus += (sender, args) =>
            {
                _msgTextBlock.Text = $"{_msgTextBlock.Text}{Environment.NewLine}GotFocus.";
            };
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var textBox = (TextBox) GetTemplateChild("PART_TextBox");
            if (textBox != null)
            {
                textBox.LostFocus += (sender, args) =>
                {
                    _msgTextBlock.Text = $"{_msgTextBlock.Text}{Environment.NewLine}InnerTextBox LostFocus.";
                };

                textBox.GotFocus += (sender, args) =>
                {
                    _msgTextBlock.Text = $"{_msgTextBlock.Text}{Environment.NewLine}InnerTextBox GotFocus.";
                };
            }

            _msgTextBlock = GetTemplateChild("PART_Msg") as TextBlock;
            _toggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;
        }
    }
}
