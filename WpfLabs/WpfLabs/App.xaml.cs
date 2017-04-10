using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Threading;

namespace WpfLabs
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            //得到当前打开的窗口实例  
            var instance = RunningProcessSpecialHelper.RunningInstance(Process.GetCurrentProcess());
            //保证永远只打开一个窗口实例  
            if (instance == null)
            {
                DispatcherHelper.Initialize();
            }
            else
            {
                //处理已经存在的窗口实例  
                RunningProcessSpecialHelper.ActiveRunningInstance(instance);

                //退出
                Environment.Exit(0);
            }
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                Debug.WriteLine(e.Exception, "UI线程全局异常");
                e.Handled = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "不可恢复的UI线程全局异常");
                MessageBox.Show("应用程序发生不可恢复的异常，将要退出！");
            }
        }

        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    Debug.WriteLine(exception, "非UI线程全局异常");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "不可恢复的非UI线程全局异常");
                MessageBox.Show("应用程序发生不可恢复的异常，将要退出！");
            }
        }
    }
}
