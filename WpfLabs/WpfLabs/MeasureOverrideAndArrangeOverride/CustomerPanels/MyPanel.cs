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
            Debug.WriteLine($"Name:{Name};availableSize:{availableSize}");

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Debug.WriteLine($"Name:{Name};finalSize:{finalSize}");

            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
        }
    }
}
