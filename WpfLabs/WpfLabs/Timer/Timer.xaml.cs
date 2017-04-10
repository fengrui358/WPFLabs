using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfLabs.Timer
{
    /// <summary>
    /// Timer.xaml 的交互逻辑
    /// </summary>
    public partial class Timer : UserControl, INotifyPropertyChanged, IDisposable
    {
        private DateTime _startTime;
        private System.Threading.Timer _timer;

        public static readonly DependencyProperty TotalSecondsProperty = DependencyProperty.Register(
            "TotalSeconds", typeof(long), typeof(Timer), new PropertyMetadata(default(long)));

        /// <summary>
        /// 累计总秒数
        /// </summary>
        public long TotalSeconds
        {
            get { return (long)GetValue(TotalSecondsProperty); }
            set { SetValue(TotalSecondsProperty, value); }
        }

        public static readonly DependencyProperty IsStartProperty = DependencyProperty.Register(
            "IsStart", typeof(bool), typeof(Timer), new PropertyMetadata(default(bool), IsStartChangedCallback));

        /// <summary>
        /// 是否启动
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <param name="dependencyPropertyChangedEventArgs"></param>
        private static void IsStartChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var timer = (Timer)dependencyObject;

            //新值变更为True，启动计时器
            if ((bool)dependencyPropertyChangedEventArgs.NewValue &&
                !(bool)dependencyPropertyChangedEventArgs.OldValue)
            {
                //启动计时器
                timer.TotalSeconds = 0;
                timer._startTime = DateTime.Now;
                timer._timer = new System.Threading.Timer(timer.ChangeTime, null, TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(500));

                //触发开始事件
                var routedEventArgs = new RoutedEventArgs { RoutedEvent = StartedEvent };
                timer.RaiseEvent(routedEventArgs);
            }
            //新值变更为False，停止计时器
            else if (!(bool)dependencyPropertyChangedEventArgs.NewValue &&
                (bool)dependencyPropertyChangedEventArgs.OldValue)
            {
                timer._timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer._timer.Dispose();
                timer._timer = null;

                //手动更新一次时间
                timer.ChangeTime(null);

                //触发停止事件
                var routedEventArgs = new RoutedEventArgs { RoutedEvent = StopedEvent };
                timer.RaiseEvent(routedEventArgs);
            }
        }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsStart
        {
            get { return (bool)GetValue(IsStartProperty); }
            set { SetValue(IsStartProperty, value); }
        }

        public static readonly RoutedEvent StartedEvent =
            EventManager.RegisterRoutedEvent("Started", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Timer));

        /// <summary>
        /// 启动事件
        /// </summary>
        public event RoutedEventHandler Started
        {
            add { AddHandler(StartedEvent, value); }
            remove { RemoveHandler(StartedEvent, value); }
        }

        public static readonly RoutedEvent StopedEvent =
            EventManager.RegisterRoutedEvent("Stoped", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Timer));

        /// <summary>
        /// 停止事件
        /// </summary>
        public event RoutedEventHandler Stoped
        {
            add { AddHandler(StopedEvent, value); }
            remove { RemoveHandler(StopedEvent, value); }
        }

        public Timer()
        {
            InitializeComponent();
        }

        private void ChangeTime(object obj)
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                TotalSeconds = (int)DateTime.Now.Subtract(_startTime).TotalSeconds;    
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                _timer.Dispose();
                _timer = null;
            }
        }
    }
}
