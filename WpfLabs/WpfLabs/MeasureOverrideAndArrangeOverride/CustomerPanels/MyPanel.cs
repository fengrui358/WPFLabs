using System.Windows;
using System.Windows.Controls;

namespace WpfLabs.MeasureOverrideAndArrangeOverride.CustomerPanels
{
    public class MyPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }
    }
}
