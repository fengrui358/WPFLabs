using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace WpfLabs.MutliUiThreadingDemo
{
    /// <summary>
    /// OtherUiThreadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class OtherUiThreadingWindow
    {
        public OtherUiThreadingWindow()
        {
            InitializeComponent();
        }

        private void OtherUiThreadingWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainThreadingInfo.Text = $"程序主线程Id：{Application.Current.Dispatcher.Thread.ManagedThreadId}";
            OtherThreadingInfo.Text = $"该窗体主线程Id：{Thread.CurrentThread.ManagedThreadId}";
        }

        private void RunNewWindow_OnClick(object sender, RoutedEventArgs e)
        {
            RunWindowHelper.RunNewWindowAsync<OtherUiThreadingWindow>();
        }

        private void TakeupUiThreading_OnClick(object sender, RoutedEventArgs e)
        {
            var i = 10;

            Cursor = Cursors.Wait;

            while (i > 0)
            {
                Thread.Sleep(1000);
                i--;
            }

            Cursor = null;
        }
    }
}
