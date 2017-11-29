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

namespace WpfLabs.FontFamilyDemo
{
    /// <summary>
    /// FontFamilyDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FontFamilyDemoWindow : Window
    {
        public FontFamilyDemoWindow()
        {
            InitializeComponent();

            DataContext = new FontFamilyDemoWindowViewModel();
        }
    }
}
