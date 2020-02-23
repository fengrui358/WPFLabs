using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace WpfLabs.ColorfulTextBlock
{
    public class ColorfulTextBlock : TextBlock
    {
        public static readonly DependencyProperty InnerParam1Property = DependencyProperty.Register(
            "InnerParam1", typeof(Run), typeof(ColorfulTextBlock), new PropertyMetadata(default(Run), ParamsChangedCallback));

        public Run InnerParam1
        {
            get => (Run) GetValue(InnerParam1Property);
            set => SetValue(InnerParam1Property, value);
        }

        public static readonly DependencyProperty InnerParam2Property = DependencyProperty.Register(
            "InnerParam2", typeof(Run), typeof(ColorfulTextBlock), new PropertyMetadata(default(Run), ParamsChangedCallback));

        public Run InnerParam2
        {
            get => (Run)GetValue(InnerParam2Property);
            set => SetValue(InnerParam2Property, value);
        }

        private static void ParamsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorfulTextBlock = (ColorfulTextBlock) d;
            if (!string.IsNullOrEmpty(colorfulTextBlock.Text))
            {
                var text = colorfulTextBlock.Text;
                var listTexts = new List<string>();
                var index = 0;

                if (colorfulTextBlock.InnerParam1 != null)
                {

                }
            }
        }

        /// <summary>
        /// 获取一段跟原始TextBlock样式一致的文本控件
        /// </summary>
        /// <returns></returns>
        //private static Run GetOriginalRun(TextBlock textBlock)
        //{
        //    var run = new Run();
        //    var binding = new Binding
        //    {
        //        Source = textBlock,

        //    };

        //    BindingOperations.SetBinding()
        //}
    }
}
