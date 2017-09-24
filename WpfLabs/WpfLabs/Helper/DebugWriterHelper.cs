using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;

namespace WpfLabs.Helper
{
    public static class DebugWriterHelper
    {
        private static LogWindow _logWindow;

        public static void WriterLine(string msg)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (_logWindow == null)
                {
                    _logWindow = new LogWindow();
                    _logWindow.Closing += LogWindowOnClosing;
                    _logWindow.Show();
                }

                _logWindow.WriteLine(msg);
            });
        }

        private static void LogWindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _logWindow.Closing -= LogWindowOnClosing;
            _logWindow = null;
        }
    }
}
