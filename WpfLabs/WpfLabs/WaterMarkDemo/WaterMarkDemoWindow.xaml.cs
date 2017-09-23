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
using WpfLabs.Helper;

namespace WpfLabs.WaterMarkDemo
{
    /// <summary>
    /// WaterMarkDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WaterMarkDemoWindow : Window
    {
        public WaterMarkDemoWindow()
        {
            InitializeComponent();
        }

        private void AttachPropertyTextBox_OnInitialized(object sender, EventArgs e)
        {
            DebugWriterHelper.WriterLine("AttachPropertyTextBox_OnInitialized");
        }

        private void AttachPropertyTextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            DebugWriterHelper.WriterLine("AttachPropertyTextBox_OnLoaded");
        }

        private void AttachPropertyTextBox_OnUnloaded(object sender, RoutedEventArgs e)
        {
            DebugWriterHelper.WriterLine("AttachPropertyTextBox_OnUnloaded");
        }
    }
}
