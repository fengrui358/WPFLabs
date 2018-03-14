using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace WpfLabs.MutliUiThreadingDemo
{
    /// <summary>
    /// 参考：https://blogs.msdn.microsoft.com/dwayneneed/2007/04/26/multithreaded-ui-hostvisual/
    /// </summary>
    [ContentProperty("Child")]
    public class VisualWrapper : FrameworkElement
    {
        public Visual Child

        {
            get { return _child; }
            set
            {
                if (_child != null)
                {
                    RemoveVisualChild(_child);
                }
                _child = value;
                if (_child != null)
                {
                    AddVisualChild(_child);
                }
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (_child != null && index == 0)
            {
                return _child;
            }
            else
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }


        protected override int VisualChildrenCount
        {
            get { return _child != null ? 1 : 0; }
        }

        private Visual _child;
    }
}