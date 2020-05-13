using System.Windows;
using System.Windows.Controls;
using WpfLabs.Base;

namespace WpfLabs.RouteEventDemo
{
    /// <summary>
    /// RouteEventDemoControl.xaml 的交互逻辑
    /// </summary>
    public partial class RouteEventDemoControl
    {
        public static readonly RoutedEvent InnerTextChangedEvent = EventManager.RegisterRoutedEvent("InnerTextChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyEventHandler<string>), typeof(RouteEventDemoControl));

        public event RoutedPropertyEventHandler<string> InnerTextChanged
        {
            add => AddHandler(InnerTextChangedEvent, value);
            remove => RemoveHandler(InnerTextChangedEvent, value);
        }

        public RouteEventDemoControl()
        {
            InitializeComponent();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = ((TextBox) e.Source).Text;
            var args = new RoutedPropertyEventArgs<string>(text) {RoutedEvent = InnerTextChangedEvent};

            RaiseEvent(args);
        }
    }
}
