using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfLabs.ColorfulTextBlock
{
    [TemplatePart(Name = InnerTextBlockName, Type = typeof(TextBlock))]
    public class ColorfulTextBlock : Control
    {
        /// <summary>
        /// 内置显示控件名称。
        /// </summary>
        public const string InnerTextBlockName = "PART_TextBlock";

        /// <summary>
        /// 内置显示文本控件。
        /// </summary>
        private TextBlock _innerTextBlock;

        static ColorfulTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorfulTextBlock), new FrameworkPropertyMetadata(typeof(ColorfulTextBlock)));
        }

        public override void OnApplyTemplate()
        {
            _innerTextBlock = GetTemplateChild(InnerTextBlockName) as TextBlock;
            ProcessHighlight();
        }

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
            colorfulTextBlock.ProcessHighlight();
        }

        /// <summary>
        /// 处理高亮显示。
        /// </summary>
        private void ProcessHighlight()
        {
            if (_innerTextBlock == null)
                return;

            _innerTextBlock.Inlines.Clear();

            if (TextFormat != null)
            {
                var paramModels = GetParamModels();
                var texts = TextFormat.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                _innerTextBlock.Inlines.Remove(_innerTextBlock.Inlines.LastInline);

                foreach (var text in texts)
                {
                    _innerTextBlock.Inlines.AddRange(GenerateRunsFromText(text, paramModels));
                }

                _innerTextBlock.Inlines.Add(new LineBreak());
            }
        }

        /// <summary>
        /// 根据文本和关键字获取文本显示Run集合。
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="paramModels">参数</param>
        /// <returns></returns>
        private IEnumerable<Run> GenerateRunsFromText(string text, List<ParamModel> paramModels)
        {
            List<Run> runList = new List<Run>();
            if (string.IsNullOrEmpty(text))
                return runList;

            if (paramModels == null || !paramModels.Any())
            {
                runList.Add(new Run(text));
            }
            else
            {
                while (!string.IsNullOrEmpty(text))
                {
                    var foundIndexs = paramModels.Select(s => new
                        {index = text.IndexOf(s.SearchKey, StringComparison.OrdinalIgnoreCase), paramModel = s}).Where(s => s.index != -1);

                    if (foundIndexs.Any())
                    {
                        var minIndex = foundIndexs.Min(s => s.index);
                        var f = foundIndexs.First(s => s.index == minIndex);

                        //增添空白文本
                        runList.Add(new Run(text.Substring(0, f.index)));

                        runList.Add(new Run(f.paramModel.Text)
                        {
                            Foreground = f.paramModel.Foreground
                        });

                        //剩下的文本继续查找
                        text = text.Substring(f.index + f.paramModel.SearchKey.Length);
                    }
                    else
                    {
                        runList.Add(new Run(text.Substring(0, text.Length)));

                        //将最后的数据加入，返回
                        break;
                    }
                }
            }

            return runList;
        }

        /// <summary>
        /// 根据传入参数获取对应的参数结构化信息
        /// </summary>
        /// <returns></returns>
        private List<ParamModel> GetParamModels()
        {
            var properties = typeof(ColorfulTextBlock).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(s => s.Name.StartsWith("InnerParam")).OrderBy(s => s.Name).ToList();

            var result = new List<ParamModel>();
            for (var i = 0; i < properties.Count; i++)
            {
                var propertyValue = (string) properties[i].GetValue(this);
                if (!string.IsNullOrEmpty(propertyValue))
                {
                    var foregroundProperty =
                        typeof(ColorfulTextBlock).GetProperty($"ForegroundParam{i}", BindingFlags.Public | BindingFlags.Instance);
                    var foreground = (Brush) foregroundProperty?.GetValue(this) ?? _innerTextBlock.Foreground;

                    result.Add(new ParamModel(propertyValue, i, foreground));
                }
            }

            return result;
        }

        private class ParamModel
        {
            public ParamModel(string text, int index, Brush foreground)
            {
                Foreground = foreground;
                Text = text;
                Index = index;
            }

            /// <summary>
            /// 参数索引
            /// </summary>
            public int Index { get; }

            public string SearchKey => $"{{{Index}}}";

            /// <summary>
            /// 参数文本
            /// </summary>
            public string Text { get; }

            /// <summary>
            /// 前景色
            /// </summary>
            public Brush Foreground { get; }
        }
    }
}