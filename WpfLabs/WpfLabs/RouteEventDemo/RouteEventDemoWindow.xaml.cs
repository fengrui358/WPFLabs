using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using WpfLabs.Base;

namespace WpfLabs.RouteEventDemo
{
    /// <summary>
    /// RouteEventDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RouteEventDemoWindow : Window
    {
        public RelayCommand<string> GetIntegerFromStringCommand { get; private set; }

        public RouteEventDemoWindow()
        {
            InitializeComponent();

            DataContext = this;

            GetIntegerFromStringCommand = new RelayCommand<string>(GetIntegerFromStringCommandHandler);
        }

        private void RouteEventDemoControl_OnInnerTextChanged(object sender, RoutedPropertyEventArgs<string> e)
        {
            TargetTextBox.Text = e.Value;
        }

        private void GetIntegerFromStringCommandHandler(string str)
        {
            TargetTextBlock.Text = str;
        }
    }
}
