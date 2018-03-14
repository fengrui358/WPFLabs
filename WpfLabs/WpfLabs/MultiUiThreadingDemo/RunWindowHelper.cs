using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfLabs.MutliUiThreadingDemo
{
    /// <summary>
    /// 参考：http://www.cnblogs.com/tcjiaan/p/7105361.html
    /// </summary>
    public class RunWindowHelper
    {
        public static Task RunNewWindowAsync<TWindow>() where TWindow : System.Windows.Window, new()
        {
            TaskCompletionSource<object> tc = new TaskCompletionSource<object>();

            // 新线程
            Thread t = new Thread(() =>
            {
                TWindow win = new TWindow();
                win.Closed += (sender, args) =>
                {
                    // 当窗口关闭后马上结束消息循环
                    System.Windows.Threading.Dispatcher.ExitAllFrames();
                };
                win.Show();

                // Run 方法必须调用，否则窗口一打开就会关闭
                // 因为没有启动消息循环
                System.Windows.Threading.Dispatcher.Run();
                // 这句话是必须的，设置Task的运算结果
                // 但由于此处不需要结果，故用null
                tc.SetResult(null);
            });
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            // 新线程启动后，将Task实例返回
            // 以便支持 await 操作符
            return tc.Task;
        }
    }
}
