using System;
using System.CodeDom;
using System.Collections.Concurrent;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Threading;

namespace WpfLabs.NotifyMsgPresenter
{
    /// <summary>
    /// 获取通知公告并展示，固定放在右下角
    /// </summary>
    [TemplatePart(Name = "PART_Container", Type = typeof(Canvas))]
    public class NotifyMsgPresenter : Control
    {
        private MockDataSource _mockDataSource;
        private Canvas _container;

        /// <summary>
        /// 在界面上展示的时间
        /// </summary>
        private int _displaySeconds;

        /// <summary>
        /// 延迟显示下一条消息的时间
        /// </summary>
        private int _delayNextMsgSecond;

        /// <summary>
        /// 是否正在显示消息
        /// </summary>
        private bool _displayingMsg;

        /// <summary>
        /// 当前收到的通知消息
        /// </summary>
        private Queue<NotifyMsgModel> _msgs = new Queue<NotifyMsgModel>();

        ///// <summary>
        ///// 最大显示数量
        ///// </summary>
        //public int MaxDisplayCount
        //{
        //    get { return (int) GetValue(MaxDisplayCountProperty); }
        //    set { SetValue(MaxDisplayCountProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MaxDisplayCount.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MaxDisplayCountProperty =
        //    DependencyProperty.Register("MaxDisplayCount", typeof(int), typeof(NotifyMsgPresenter),
        //        new PropertyMetadata(3));

        /// <summary>
        /// 在界面显示并停留的秒数
        /// </summary>
        public int DisplaySeconds
        {
            get { return (int) GetValue(DisplaySecondsProperty); }
            set { SetValue(DisplaySecondsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplaySeconds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplaySecondsProperty =
            DependencyProperty.Register("DisplaySeconds", typeof(int), typeof(NotifyMsgPresenter),
                new PropertyMetadata(5, (o, args) => ((NotifyMsgPresenter) o)._displaySeconds = (int) args.NewValue));

        /// <summary>
        /// 延迟显示下一条消息的时间
        /// </summary>
        public int DelayNextMsgSecond
        {
            get { return (int)GetValue(DelayNextMsgSecondProperty); }
            set { SetValue(DelayNextMsgSecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DelayNextMsgSecond.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DelayNextMsgSecondProperty =
            DependencyProperty.Register("DelayNextMsgSecond", typeof(int), typeof(NotifyMsgPresenter),
                new PropertyMetadata(1, (o, args) => ((NotifyMsgPresenter) o)._delayNextMsgSecond = (int) args.NewValue));

        static NotifyMsgPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotifyMsgPresenter),
                new FrameworkPropertyMetadata(typeof(NotifyMsgPresenter)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _container = GetTemplateChild("PART_Container") as Canvas;
        }

        public NotifyMsgPresenter()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //订阅数据源
            _mockDataSource = new MockDataSource();
            _mockDataSource.NewMsgReached += OnNewMsgReached;
        }

        private async void DisplayMsg(NotifyMsgModel msg)
        {
            if (msg != null)
            {
                _displayingMsg = true;

                //展示信息
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    var item = new NotifyMsgItem(msg)
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Opacity = 1,
                        Margin = new Thickness(0, ActualHeight + 10, 0, 0)
                    };

                    _container.Children.Add(item);

                    //向上滑动动画总显示时间减1秒
                    var floatAnimation = new ThicknessAnimation();
                    floatAnimation.From = new Thickness(0, ActualHeight + 10, 0, 0);
                    floatAnimation.To = new Thickness(0);
                    floatAnimation.Duration = new Duration(TimeSpan.FromSeconds(_displaySeconds - 1));

                    item.BeginAnimation(MarginProperty, floatAnimation);
                });

                await Task.Delay(TimeSpan.FromSeconds(_delayNextMsgSecond));

                //判断缓存中是否还有待显示信息
                NotifyMsgModel nextMsg = null;
                lock (_msgs)
                {
                    if (_msgs.Count > 0)
                    {
                        nextMsg = _msgs.Dequeue();
                    }
                }
                if (nextMsg != null)
                {
                    DisplayMsg(nextMsg);
                }
                else
                {
                    _displayingMsg = false;
                }

                await Task.Delay(TimeSpan.FromSeconds(_displaySeconds - _delayNextMsgSecond - 1));

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    var item = _container.Children[0];

                    //显示完成，隐藏动画耗时1秒
                    var opacityAnimation = new DoubleAnimation();
                    opacityAnimation.From = 1;
                    opacityAnimation.To = 0;
                    opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
                    item.BeginAnimation(OpacityProperty, opacityAnimation);
                });

                //等待主线程完成
                await Task.Delay(TimeSpan.FromSeconds(1));

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    var item = _container.Children[0];
                    _container.Children.Remove(item);
                });
            }
        }

        /// <summary>
        /// 有新的通知消息到达
        /// </summary>
        /// <param name="notifyMsgModel"></param>
        private void OnNewMsgReached(NotifyMsgModel notifyMsgModel)
        {
            //判断计时器是否正在运行
            //如果没有运行代表当前没有显示消息，则直接显示否则加入缓存
            if (!_displayingMsg)
            {
                DisplayMsg(notifyMsgModel);
            }
            else
            {
                //当前正在处理信息显示，则直接加到队尾
                lock (_msgs)
                {
                    _msgs.Enqueue(notifyMsgModel);
                }
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //取消订阅数据源
            _mockDataSource.NewMsgReached -= OnNewMsgReached;
            _mockDataSource.Dispose();
        }
    }
}