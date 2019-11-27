using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLabs.ItemsControlWithTimeline
{
    /// <summary>
    /// Interaction logic for TimelineNode.xaml
    /// </summary>
    public partial class TimelineNode : UserControl
    {
        public static readonly DependencyProperty ShowAboveProperty = DependencyProperty.Register(
            "ShowAbove", typeof(bool), typeof(TimelineNode), new PropertyMetadata(true, ShowAbovePropertyChangedCallback));

        private static void ShowAbovePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TimelineNode) d).Above.Visibility = (bool) e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool ShowAbove
        {
            get => (bool) GetValue(ShowAboveProperty);
            set => SetValue(ShowAboveProperty, value);
        }

        public static readonly DependencyProperty ShowBelowProperty = DependencyProperty.Register(
            "ShowBelow", typeof(bool), typeof(TimelineNode), new PropertyMetadata(true, ShowBelowPropertyChangedCallback));

        private static void ShowBelowPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TimelineNode)d).Below.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool ShowBelow
        {
            get => (bool)GetValue(ShowBelowProperty);
            set => SetValue(ShowBelowProperty, value);
        }

        public TimelineNode()
        {
            InitializeComponent();
        }
    }
}
