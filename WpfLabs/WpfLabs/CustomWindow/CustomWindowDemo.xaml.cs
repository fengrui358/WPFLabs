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

namespace WpfLabs.CustomWindow
{
    /// <summary>
    /// Interaction logic for CustomWindowDemo.xaml
    /// https://blog.walterlv.com/post/wpf-simulate-native-window-title-bar-buttons.html
    /// https://blog.walterlv.com/post/wpf-simulate-native-window-style-using-window-chrome.html
    /// https://walterlv.com/post/wpf-transparent-window-without-allows-transparency.html?tdsourcetag=s_pctim_aiomsg
    /// </summary>
    public partial class CustomWindowDemo : Window
    {
        public CustomWindowDemo()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
