using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLabs.NotifyMsgPresenter
{
    /// <summary>
    /// 获取通知公告并展示，固定放在右下角
    /// </summary>
    public class NotifyMsgPresenter : Control
    {
        private MockDataSource _mockDataSource;

        /// <summary>
        /// 当前收到的通知消息
        /// </summary>
        private ConcurrentQueue<NotifyMsgModel> _msgs = new ConcurrentQueue<NotifyMsgModel>();

        /// <summary>
        /// 最大显示数量
        /// </summary>
        public int MaxDisplayCount
        {
            get { return (int)GetValue(MaxDisplayCountProperty); }
            set { SetValue(MaxDisplayCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxDisplayCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxDisplayCountProperty =
            DependencyProperty.Register("MaxDisplayCount", typeof(int), typeof(NotifyMsgPresenter), new PropertyMetadata(3));

        /// <summary>
        /// 在界面显示并停留的秒数
        /// </summary>
        public int DisplaySeconds
        {
            get { return (int)GetValue(DisplaySecondsProperty); }
            set { SetValue(DisplaySecondsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplaySeconds.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplaySecondsProperty =
            DependencyProperty.Register("DisplaySeconds", typeof(int), typeof(NotifyMsgPresenter), new PropertyMetadata(3));

        

        static NotifyMsgPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotifyMsgPresenter), new FrameworkPropertyMetadata(typeof(NotifyMsgPresenter)));
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

        /// <summary>
        /// 有新的通知消息到达
        /// </summary>
        /// <param name="notifyMsgModel"></param>
        private void OnNewMsgReached(NotifyMsgModel notifyMsgModel)
        {
            
        }

        private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //取消订阅数据源
            _mockDataSource.NewMsgReached -= OnNewMsgReached;
            _mockDataSource.Dispose();
        }
    }
}
