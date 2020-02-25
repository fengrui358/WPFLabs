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

namespace WpfLabs.ColorfulTextBlock
{
    /// <summary>
    /// ColorfulTextBlockWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ColorfulTextBlockWindow : Window
    {
        public string Text1 { get; } = "wasd{0}asdada{2}dsaada{1}s{2}a";

        public string Param0 { get; } = "Param0";
        public string Param2 { get; } = "Param2";

        public ColorfulTextBlockWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
