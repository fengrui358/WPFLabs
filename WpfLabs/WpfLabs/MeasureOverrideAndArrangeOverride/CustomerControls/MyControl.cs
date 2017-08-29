using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfLabs.MeasureOverrideAndArrangeOverride.CustomerControls
{
    public class MyControl : Control
    {
        protected override Size MeasureOverride(Size constraint)
        {
            Debug.WriteLine($"Name:{Name};constraintSize:{constraint}");

            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Debug.WriteLine($"Name:{Name};arrangeBoundsSize:{arrangeBounds}");

            return base.ArrangeOverride(arrangeBounds);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Background, null, new Rect(new Size(100,100)));
        }
    }
}
