using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WpfLabs.PopupTextBox
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.PopupTextBox"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.PopupTextBox;assembly=WpfLabs.PopupTextBox"
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
    ///     <MyNamespace:PopupTextBox/>
    ///
    /// </summary>
    public class PopupTextBox : Control
    {
        private bool _isTabKeyDown;
        private Popup _popup;
        private TextBox _multiTextBox;

        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(nameof(MaxLength), typeof(int), typeof(PopupTextBox),
            (PropertyMetadata) new FrameworkPropertyMetadata((object) 0));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(PopupTextBox),
            (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal));

        public static readonly DependencyProperty NextUiElementProperty = DependencyProperty.Register(
            "NextUiElement", typeof(UIElement), typeof(PopupTextBox), new PropertyMetadata(default(UIElement)));

        /// <summary>
        /// 用于tab转移焦点
        /// </summary>
        public UIElement NextUiElement
        {
            get => (UIElement) GetValue(NextUiElementProperty);
            set => SetValue(NextUiElementProperty, value);
        }

        static PopupTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupTextBox), new FrameworkPropertyMetadata(typeof(PopupTextBox)));
        }

        public int MaxLength
        {
            get => (int)this.GetValue(MaxLengthProperty);
            set => this.SetValue(MaxLengthProperty, (object)value);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, (object)value);
        }

        public override void OnApplyTemplate()
        {
            _popup = (Popup) GetTemplateChild("Popup");
            _multiTextBox = (TextBox) GetTemplateChild("MultiTextBox");

            if (_multiTextBox != null)
            {
                _multiTextBox.KeyDown += MultiTextBoxOnKeyDown;
                _multiTextBox.KeyUp += MultiTextBoxOnKeyUp;
            }

            GotFocus += OnGotFocus;
            LostFocus += OnLostFocus;

            base.OnApplyTemplate();
        }

        private void MultiTextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                _isTabKeyDown = true;

                Task.Run(async () =>
                {
                    await Task.Delay(400);
                    _isTabKeyDown = false;
                });
            }
        }

        private void MultiTextBoxOnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab && _isTabKeyDown)
            {
                _isTabKeyDown = false;

                //转移tab焦点
                if (NextUiElement != null)
                {
                    NextUiElement.Focus();
                }
                else
                {
                    var parent = LogicalTreeHelper.GetParent(this) as Panel;
                    var index = -1;
                    for (var i = 0; i < parent.Children.Count; i++)
                    {
                        if (parent.Children[i] == this)
                        {
                            index = i + 1;
                            break;
                        }
                    }

                    if (index >= 0 && index < parent.Children.Count)
                    {
                        parent.Children[index].Focus();
                    }
                }
            }
        }


        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            _popup.IsOpen = false;

            //移除输入开头的回车
            Text = Text?.TrimStart();
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            _multiTextBox.Focus();
            _popup.IsOpen = true;
            _multiTextBox.Focus();
        }
    }
}
