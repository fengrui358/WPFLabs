using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfLabs.MeasureOverrideAndArrangeOverride.CustomerPanels
{
    public class MyPanel : Panel
    {
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
    }
}
