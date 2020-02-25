using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace WpfLabs.ColorfulTextBlock
{
    public class ColorfulTextBlock : TextBlock
    {
        public static readonly DependencyProperty InnerParam0Property = DependencyProperty.Register(
            "InnerParam0", typeof(Run), typeof(ColorfulTextBlock), new PropertyMetadata(default(Run), ParamsChangedCallback));

        public Run InnerParam0
        {
            get => (Run) GetValue(InnerParam0Property);
            set => SetValue(InnerParam0Property, value);
        }

        public static readonly DependencyProperty InnerParam1Property = DependencyProperty.Register(
            "InnerParam1", typeof(Run), typeof(ColorfulTextBlock), new PropertyMetadata(default(Run), ParamsChangedCallback));

        public Run InnerParam1
        {
            get => (Run)GetValue(InnerParam1Property);
            set => SetValue(InnerParam1Property, value);
        }

        public static readonly DependencyProperty InnerParam2Property = DependencyProperty.Register(
            "InnerParam2", typeof(Run), typeof(ColorfulTextBlock), new PropertyMetadata(default(Run), ParamsChangedCallback));

        public Run InnerParam2
        {
            get => (Run) GetValue(InnerParam2Property);
            set => SetValue(InnerParam2Property, value);
        }

        private static void ParamsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorfulTextBlock = (ColorfulTextBlock) d;
            colorfulTextBlock.ChangeInlines();
        }

        public ColorfulTextBlock()
        {
            IsVisibleChanged += OnIsVisibleChanged;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                ChangeInlines();
            }
        }

        private void ChangeInlines()
        {
            if (!string.IsNullOrEmpty(Text))
            {
                var properties = typeof(ColorfulTextBlock).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(s => s.Name.StartsWith("InnerParam")).OrderBy(s => s.Name);
                var runs = new Dictionary<int, Run>();

                foreach (var propertyInfo in properties)
                {
                    var propertyValue = (Run)propertyInfo.GetValue(this);
                    if (propertyValue != null)
                    {
                        var i = int.Parse(propertyInfo.Name.Remove(0, "InnerParam".Length));
                        runs.Add(i, propertyValue);
                    }
                }

                var inlines = new List<Run>();
                var text = Text;
                var lastMatchIndex = 0;

                var matchs = Regex.Matches(text, "{\\d+}");

                foreach (Match match in matchs)
                {
                    //非参数字符串
                    var t = $"{text.Substring(lastMatchIndex, match.Index - lastMatchIndex)}";

                    var paramIndex = int.Parse(match.Value.Substring(1, match.Value.Length - 2));
                    if (runs.ContainsKey(paramIndex))
                    {
                        inlines.Add(new Run(t));
                        inlines.Add(runs[paramIndex]);

                        lastMatchIndex = match.Index + match.Length;
                    }
                }

                var last = text.Substring(lastMatchIndex, text.Length - lastMatchIndex);
                if (!string.IsNullOrEmpty(last))
                {
                    inlines.Add(new Run(last));
                }

                Inlines.Clear();
                Inlines.AddRange(inlines);
            }
        }
    }
}
