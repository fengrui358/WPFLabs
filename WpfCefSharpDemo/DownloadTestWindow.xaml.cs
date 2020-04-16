using System.Windows;

namespace WpfCefSharpDemo
{
    /// <summary>
    /// DownloadTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DownloadTestWindow : Window
    {
        public DownloadTestWindow()
        {
            InitializeComponent();

            Browser.DownloadHandler = new DownloadHandler();
        }
    }
}
