using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Threading;

namespace WpfLabs.Helper
{
    /// <summary>
    /// LogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogWindow : Window
    {
        private bool _canAutoScroll = true;
        private System.Threading.Timer _operateTimer;

        public LogWindow()
        {
            _operateTimer = new System.Threading.Timer(DisableAutoScroll);

            InitializeComponent();

            Left = SystemParameters.PrimaryScreenWidth - Width;
            Top = 0;
        }

        private void LogTextBoxOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            if (_canAutoScroll)
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() => { LogTextBox.ScrollToEnd(); });
            }
        }

        private void DisableAutoScroll(object obj)
        {
            _canAutoScroll = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            _canAutoScroll = false;

            if (_operateTimer != null)
            {
                _operateTimer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
                _operateTimer.Dispose();
                _operateTimer = null;
            }
        }

        public void WriteLine(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                DispatcherHelper.CheckBeginInvokeOnUI(
                    () =>
                    {
                        if (string.IsNullOrEmpty(LogTextBox.Text))
                        {
                            LogTextBox.Text = $"{DateTime.Now:yyyyMMdd-HH:mm:ss}-【{msg}】";
                        }
                        else
                        {
                            LogTextBox.AppendText($"{Environment.NewLine}{DateTime.Now:yyyyMMdd-HH:mm:ss}-【{msg}】");
                        }
                    }
                );
            }
        }

        private void LogTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            _canAutoScroll = true;

            _operateTimer.Change(TimeSpan.FromSeconds(5), Timeout.InfiniteTimeSpan);
        }
    }
}