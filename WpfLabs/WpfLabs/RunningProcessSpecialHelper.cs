using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfLabs
{
    class RunningProcessSpecialHelper
    {
        #region 窗体打开模式

        private const int SW_HIDE = 0;
        private const int SW_NORMAL = 1;
        private const int SW_MAXIMIZE = 3;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_SHOW = 5;
        private const int SW_MINIMIZE = 6;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWDEFAULT = 10;

        #endregion

        /// <summary>处理已经存在的窗口实例</summary>
        /// <returns></returns>
        public static Process RunningInstance(Process current)
        {
            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                if (process.Id != current.Id)
                {
                    Debug.WriteLine("发现同一进程名字的不同进程实例,已有进程位置{0},当前进程位置{1}", (object) process.MainModule.FileName,
                        (object) current.MainModule.FileName);
                    return process;
                }
            }
            return (Process) null;
        }

        /// <summary>将已运行实例切换到显示状态,并最大化</summary>
        /// <param name="instance"></param>
        public static void ActiveRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_RESTORE);
            SetForegroundWindow(instance.MainWindowHandle);
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
