using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfLabs.MutliUiThreadingDemo
{
    /// <summary>
    /// MultiUiThreadingDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MultiUiThreadingDemoWindow
    {
        private readonly AutoResetEvent _event = new AutoResetEvent(false);
        private Thread _otherThreading;
        private Dispatcher _otherDispatcher;

        public MultiUiThreadingDemoWindow()
        {
            InitializeComponent();

            Closed += (sender, args) =>
            {
                _otherThreading = null;
                _otherDispatcher.BeginInvoke(new Action(Dispatcher.ExitAllFrames));
                _otherDispatcher = null;
            };
        }

        private void RunNewWindow_OnClick(object sender, RoutedEventArgs e)
        {
            RunWindowHelper.RunNewWindowAsync<OtherUiThreadingWindow>();
        }

        private void TakeupUiThreading_OnClick(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            var i = 10;

            while (i > 0)
            {
                Thread.Sleep(1000);
                i--;
            }
            Cursor = null;
        }

        private void MutliUiThreadingDemoWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            VisualWrapper.Child = GetElementOnOtherUiThread();
        }

        private HostVisual GetElementOnOtherUiThread()
        {
            // Create the HostVisual that will "contain" the VisualTarget
            // on the worker thread.
            HostVisual hostVisual = new HostVisual();

            // Spin up a worker thread, and pass it the HostVisual that it
            // should be part of.
            _otherThreading = new Thread(ElementOnOtherUiThread);
            _otherThreading.SetApartmentState(ApartmentState.STA);
            _otherThreading.IsBackground = true;
            _otherThreading.Start(hostVisual);

            // Wait for the worker thread to spin up and create the VisualTarget.
            _event.WaitOne();

            ThreadingInfo.Text = $"当前窗体主线程Id：{Thread.CurrentThread.ManagedThreadId}，TextBlock线程Id：{_otherThreading.ManagedThreadId}";

            return hostVisual;
        }

        private void ElementOnOtherUiThread(object arg)
        {
            _otherDispatcher = Dispatcher.CurrentDispatcher;
            // Create the VisualTargetPresentationSource and then signal the
            // calling thread, so that it can continue without waiting for us.
            HostVisual hostVisual = (HostVisual)arg;
            VisualTargetPresentationSource visualTargetPs = new VisualTargetPresentationSource(hostVisual);

            _event.Set();

            // Create a MediaElement and use it as the root visual for the
            // VisualTarget.
            var txt = new TextBlock();

            visualTargetPs.RootVisual = txt;

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(50);

                    txt.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        txt.Text = $"控件线程：{Thread.CurrentThread.ManagedThreadId}--{Guid.NewGuid():N}";
                    }));

                    if (_otherThreading == null)
                    {
                        break;
                    }
                }
            });

            // Run a dispatcher for this worker thread.  This is the central
            // processing loop for WPF.
            Dispatcher.Run();
        }
    }
}
