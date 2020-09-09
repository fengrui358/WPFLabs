using System.Windows;

namespace WpfLabs.DependencyPropertyInheritsDemo
{
    public class DependencyProperties : DependencyObject
    {
        public static readonly DependencyProperty TargetProperty = DependencyProperty.RegisterAttached(
            "Target", typeof(TargetDependencyProperty), typeof(DependencyProperties),
            new FrameworkPropertyMetadata(default(TargetDependencyProperty),
                FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));
    }

    public class TargetDependencyProperty
    {
        public string Test { get; set; }
    }
}
