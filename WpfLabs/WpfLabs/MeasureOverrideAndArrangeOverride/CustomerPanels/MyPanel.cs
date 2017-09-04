using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfLabs.Helper;

namespace WpfLabs.MeasureOverrideAndArrangeOverride.CustomerPanels
{
    public class MyPanel : Panel
    {
        private Pen LeftPenCache { get; set; }

        private Pen RightPenCache { get; set; }

        private Pen TopPenCache { get; set; }

        private Pen BottomPenCache { get; set; }

        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(MyPanel),
                (PropertyMetadata) new FrameworkPropertyMetadata((object) new Thickness(),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(MyPanel.OnClearPenCache)),
                new ValidateValueCallback(MyPanel.IsThicknessValid));

        /// <summary>
        ///   获取或设置 <see cref="T:System.Windows.Controls.Border" /> 的相对 <see cref="T:System.Windows.Thickness" />。
        /// </summary>
        /// <returns>
        ///   描述 <see cref="T:System.Windows.Controls.Border" /> 的边界的宽度的 <see cref="T:System.Windows.Thickness" />。
        ///    此属性没有默认值。
        /// </returns>
        public Thickness BorderThickness
        {
            get
            {
                return (Thickness)this.GetValue(MyPanel.BorderThicknessProperty);
            }
            set
            {
                this.SetValue(MyPanel.BorderThicknessProperty, (object)value);
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            MessageDisplayer.Instance.AppendLine($"Name:{Name};availableSize:{availableSize}");

            foreach (UIElement internalChild in InternalChildren)
            {
                internalChild.Measure(availableSize);
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            MessageDisplayer.Instance.AppendLine($"Name:{Name};finalSize:{finalSize}");

            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext dc)
        {
            Brush background = this.Background;
            if (background == null)
                return;
            Size renderSize = this.RenderSize;
            dc.DrawRectangle(background, (Pen)null, new Rect(0.0, 0.0, renderSize.Width, renderSize.Height));
        }

        private static bool IsThicknessValid(object value)
        {
            return ThicknessHelper.IsValid((Thickness)value, false, false, false, false);
        }

        private static void OnClearPenCache(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyPanel myPanel = (MyPanel) d;
            myPanel.LeftPenCache = (Pen)null;
            myPanel.RightPenCache = (Pen)null;
            myPanel.TopPenCache = (Pen)null;
            myPanel.BottomPenCache = (Pen)null;
        }
    }
}
