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
using GalaSoft.MvvmLight.CommandWpf;

namespace WpfLabs.RichTextBoxDemo
{
    /// <summary>
    /// RichTextBoxDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RichTextBoxDemoWindow : Window
    {
        public RelayCommand SendCommand { get; private set; }

        public RichTextBoxDemoWindow()
        {
            InitializeComponent();
            DataContext = this;

            SendCommand = new RelayCommand(SendCommandHandler);
        }

        private void SendCommandHandler()
        {

        }
    }
}
