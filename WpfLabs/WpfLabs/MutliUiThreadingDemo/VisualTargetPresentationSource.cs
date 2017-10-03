using System;
using System.Windows;
using System.Windows.Media;

namespace WpfLabs.MutliUiThreadingDemo
{
    public class VisualTargetPresentationSource : PresentationSource
    {
        public VisualTargetPresentationSource(HostVisual hostVisual)
        {
            _visualTarget = new VisualTarget(hostVisual);
        }

        public override Visual RootVisual
        {
            get
            {
                return _visualTarget.RootVisual;
            }

            set
            {
                Visual oldRoot = _visualTarget.RootVisual;

                // Set the root visual of the VisualTarget.  This visual will
                // now be used to visually compose the scene.
                _visualTarget.RootVisual = value;

                // Tell the PresentationSource that the root visual has
                // changed.  This kicks off a bunch of stuff like the
                // Loaded event.
                RootChanged(oldRoot, value);

                // Kickoff layout...
                UIElement rootElement = value as UIElement;

                if (rootElement != null)
                {
                    rootElement.Measure(new Size(Double.PositiveInfinity,
                        Double.PositiveInfinity));

                    rootElement.Arrange(new Rect(rootElement.DesiredSize));
                }
            }
        }

        protected override CompositionTarget GetCompositionTargetCore()
        {
            return _visualTarget;
        }

        public override bool IsDisposed
        {
            get
            {
                // We don't support disposing this object.
                return false;
            }
        }

        private readonly VisualTarget _visualTarget;
    }
}
