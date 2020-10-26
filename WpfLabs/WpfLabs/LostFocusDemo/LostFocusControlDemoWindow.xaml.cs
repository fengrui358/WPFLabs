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
using System.Windows.Shapes;

namespace WpfLabs.LostFocusDemo
{
    /// <summary>
    /// LostFocusControlDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LostFocusControlDemoWindow : Window
    {
        public LostFocusControlDemoWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnGotFocus(object sender, RoutedEventArgs e)
        {
            TextContainer.Text = $"{TextContainer.Text}{Environment.NewLine}{DateTime.Now.Ticks} {sender.GetType().Name} GotFocus";
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            TextContainer.Text = $"{TextContainer.Text}{Environment.NewLine}{DateTime.Now.Ticks} {sender.GetType().Name} LostFocus";
        }
    }
}
