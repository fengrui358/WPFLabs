using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfLabs.ColorfulTextBlock
{
    public class ColorfulTextBlock : ContentControl
    {
        private readonly WrapPanel _wrapPanel;

        #region Param0

        public static readonly DependencyProperty InnerParam0Property = DependencyProperty.Register(nameof(InnerParam0),
            typeof(string), typeof(ColorfulTextBlock),
            new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                ParamsChangedCallback));

        public string InnerParam0
        {
            get => (string) GetValue(InnerParam0Property);
            set => SetValue(InnerParam0Property, value);
        }

        public static readonly DependencyProperty ForegroundParam0Property = DependencyProperty.Register(
            "ForegroundParam0", typeof(Brush), typeof(ColorfulTextBlock),
            new FrameworkPropertyMetadata(default(Brush), ParamsChangedCallback));

        public Brush ForegroundParam0
        {
            get => (Brush)GetValue(ForegroundParam0Property);
            set => SetValue(ForegroundParam0Property, value);
        }

        #endregion

        #region Param1

        public static readonly DependencyProperty InnerParam1Property = DependencyProperty.Register(nameof(InnerParam1),
            typeof(string), typeof(ColorfulTextBlock),
            new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                ParamsChangedCallback));

        public string InnerParam1
        {
            get => (string) GetValue(InnerParam1Property);
            set => SetValue(InnerParam1Property, value);
        }

        public static readonly DependencyProperty ForegroundParam1Property = DependencyProperty.Register(
            "ForegroundParam1", typeof(Brush), typeof(ColorfulTextBlock),
            new FrameworkPropertyMetadata(default(Brush), ParamsChangedCallback));

        public Brush ForegroundParam1
        {
            get => (Brush)GetValue(ForegroundParam1Property);
            set => SetValue(ForegroundParam1Property, value);
        }

        #endregion

        #region Param2

        public static readonly DependencyProperty InnerParam2Property = DependencyProperty.Register(nameof(InnerParam2),
            typeof(string), typeof(ColorfulTextBlock),
            new FrameworkPropertyMetadata(string.Empty,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                ParamsChangedCallback));

        public string InnerParam2
        {
            get => (string) GetValue(InnerParam2Property);
            set => SetValue(InnerParam2Property, value);
        }

        public static readonly DependencyProperty ForegroundParam2Property = DependencyProperty.Register(
            "ForegroundParam2", typeof(Brush), typeof(ColorfulTextBlock),
            new FrameworkPropertyMetadata(default(Brush), ParamsChangedCallback));

        public Brush ForegroundParam2
        {
            get => (Brush) GetValue(ForegroundParam2Property);
            set => SetValue(ForegroundParam2Property, value);
        }

        #endregion

        public static readonly DependencyProperty TextFormatProperty = DependencyProperty.Register(
            "TextFormat", typeof(string), typeof(ColorfulTextBlock),
            new PropertyMetadata(default(string), ParamsChangedCallback));

        public string TextFormat
        {
            get => (string) GetValue(TextFormatProperty);
            set => SetValue(TextFormatProperty, value);
        }

        private static void ParamsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorfulTextBlock = (ColorfulTextBlock) d;
            colorfulTextBlock.ChangeInlines();
        }

        public ColorfulTextBlock()
        {
            _wrapPanel = new WrapPanel();

            Content = _wrapPanel;
        }

        private void ChangeInlines()
        {
            _wrapPanel.Children.Clear();

            if (!string.IsNullOrEmpty(TextFormat))
            {
                var properties = typeof(ColorfulTextBlock).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(s => s.Name.StartsWith("InnerParam")).OrderBy(s => s.Name);
                var runs = new Dictionary<int, TextBlock>();

                foreach (var propertyInfo in properties)
                {
                    var propertyValue = (string) propertyInfo.GetValue(this);
                    if (!string.IsNullOrEmpty(propertyValue))
                    {
                        var i = int.Parse(propertyInfo.Name.Remove(0, "InnerParam".Length));
                        var foregroundPropertyInfo =
                            typeof(ColorfulTextBlock).GetProperty($"ForegroundParam{i}",
                                BindingFlags.Public | BindingFlags.Instance);

                        var textBlock = new TextBlock
                        {
                            Text = propertyValue
                        };

                        if (foregroundPropertyInfo != null)
                        {
                            var foreground = (Brush) foregroundPropertyInfo.GetValue(this);
                            if (foreground != null)
                            {
                                textBlock.Foreground = foreground;
                            }
                        }

                        runs.Add(i, textBlock);
                    }
                }

                var inlines = new List<TextBlock>();
                var text = TextFormat;
                var lastMatchIndex = 0;

                var matchs = Regex.Matches(text, "{\\d+}");

                foreach (Match match in matchs)
                {
                    //非参数字符串
                    var t = $"{text.Substring(lastMatchIndex, match.Index - lastMatchIndex)}";

                    var paramIndex = int.Parse(match.Value.Substring(1, match.Value.Length - 2));
                    if (runs.ContainsKey(paramIndex))
                    {
                        if (!string.IsNullOrEmpty(t))
                        {
                            inlines.Add(new TextBlock {Text = t});
                        }

                        inlines.Add(runs[paramIndex]);

                        lastMatchIndex = match.Index + match.Length;
                    }
                }

                var last = text.Substring(lastMatchIndex, text.Length - lastMatchIndex);
                if (!string.IsNullOrEmpty(last))
                {
                    inlines.Add(new TextBlock {Text = last});
                }

                foreach (var textBlock in inlines)
                {
                    _wrapPanel.Children.Add(textBlock);
                }
            }
        }
    }
}