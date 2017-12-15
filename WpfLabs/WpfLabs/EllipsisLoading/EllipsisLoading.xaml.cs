using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfLabs.EllipsisLoading
{
    /// <summary>
    /// EllipsisLoading.xaml 的交互逻辑
    /// </summary>
    public partial class EllipsisLoading
    {
        #region 字段

        private DispatcherTimer _dispatcherTimer;

        #endregion

        #region 是否激活

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(EllipsisLoading), new FrameworkPropertyMetadata(default(bool), IsActiveChangedCallback));

        public bool IsActive
        {
            get { return (bool) GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void IsActiveChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ellipsisLoading = (EllipsisLoading)dependencyObject;

            var isActive = (bool) dependencyPropertyChangedEventArgs.NewValue;
            if (isActive)
            {
                if (ellipsisLoading._dispatcherTimer == null)
                {
                    ellipsisLoading._dispatcherTimer =
                        new DispatcherTimer(TimeSpan.FromMilliseconds(ellipsisLoading.IntervalMillionSeconds),
                            DispatcherPriority.Normal, ellipsisLoading.FlashingCallback, Dispatcher.CurrentDispatcher);
                }

                ellipsisLoading.Visibility = Visibility.Visible;
                ellipsisLoading._dispatcherTimer.Start();
            }
            else
            {
                ellipsisLoading.Visibility = Visibility.Collapsed;
                ellipsisLoading._dispatcherTimer?.Stop();
            }
        }

        /// <summary>
        /// 控制隐藏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void FlashingCallback(object sender, EventArgs eventArgs)
        {
            if (Container.Children.Count == 0)
            {
                return;
            }

            var lastIsVisibleIndex = -1;

            for (var i = 0; i < Container.Children.Count; i++)
            {
                if (Container.Children[i].IsVisible)
                {
                    lastIsVisibleIndex = i;
                }
                else
                {
                    break;
                }
            }

            if (lastIsVisibleIndex + 1 == Container.Children.Count)
            {
                //如果到达末尾则全部隐藏
                foreach (UIElement containerChild in Container.Children)
                {
                    containerChild.Visibility = IsHidePlaceholder
                        ? Visibility.Hidden
                        : Visibility.Collapsed;
                }
            }
            else
            {
                Container.Children[lastIsVisibleIndex + 1].Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region 省略号数量

        public static readonly DependencyProperty EllipsisCountProperty = DependencyProperty.Register(
            "EllipsisCount", typeof(int), typeof(EllipsisLoading),
            new FrameworkPropertyMetadata(3, EllipsisCountPropertyChangedCallback), ValidateValueCallback);

        /// <summary>
        /// 省略号数量
        /// </summary>
        public int EllipsisCount
        {
            get { return (int) GetValue(EllipsisCountProperty); }
            set { SetValue(EllipsisCountProperty, value); }
        }

        private static void EllipsisCountPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ellipsisLoading = (EllipsisLoading)dependencyObject;
            var oldValue = (int) dependencyPropertyChangedEventArgs.OldValue;
            var newValue = (int)dependencyPropertyChangedEventArgs.NewValue;

            if (newValue > oldValue)
            {
                //新增
                for (int i = oldValue; i < newValue; i++)
                {
                    ellipsisLoading.Container.Children.Add(
                        new TextBlock
                        {
                            Text = ".",
                            Visibility = ellipsisLoading.IsHidePlaceholder ? Visibility.Hidden : Visibility.Collapsed
                        });
                }
            }
            else
            {
                //减少
                for (var i = ellipsisLoading.Container.Children.Count - 1; i >= newValue; i--)
                {
                    ellipsisLoading.Container.Children.RemoveAt(i);
                }
            }
        }

        #endregion

        #region 动画间隔时长

        public static readonly DependencyProperty IntervalMillionSecondsProperty = DependencyProperty.Register(
            "IntervalMillionSeconds", typeof(int), typeof(EllipsisLoading),
            new FrameworkPropertyMetadata(400, FrameworkPropertyMetadataOptions.AffectsArrange,
                IntervalMillionSeconsPropertyChangedCallback, IntervalMillionSeconsCoerceValueCallback), ValidateValueCallback);

        private static object IntervalMillionSeconsCoerceValueCallback(DependencyObject dependencyObject, object baseValue)
        {
            if ((int)baseValue <= 50)
            {
                return 50;
            }

            return baseValue;
        }

        private static void IntervalMillionSeconsPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ellipsisLoading = (EllipsisLoading)dependencyObject;

            if (ellipsisLoading._dispatcherTimer != null)
            {
                ellipsisLoading._dispatcherTimer.Interval =
                    TimeSpan.FromMilliseconds((int)dependencyPropertyChangedEventArgs.NewValue);
            }
        }

        /// <summary>
        /// 动画间隔毫秒数
        /// </summary>
        public int IntervalMillionSeconds
        {
            get { return (int)GetValue(IntervalMillionSecondsProperty); }
            set { SetValue(IntervalMillionSecondsProperty, value); }
        }

        private static bool ValidateValueCallback(object value)
        {
            if ((int)value < 0)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 隐藏时是否占位

        public static readonly DependencyProperty IsHidePlaceholderProperty = DependencyProperty.Register(
            "IsHidePlaceholder", typeof(bool), typeof(EllipsisLoading), new FrameworkPropertyMetadata(true, IsHidePlaceholderPropertyChangedCallback));

        private static void IsHidePlaceholderPropertyChangedCallback(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var ellipsisLoading = (EllipsisLoading) dependencyObject;
            var isHidePlaceholder = (bool) dependencyPropertyChangedEventArgs.NewValue;

            if (isHidePlaceholder)
            {
                foreach (UIElement uiElement in ellipsisLoading.Container.Children)
                {
                    if (uiElement.Visibility == Visibility.Collapsed)
                    {
                        uiElement.Visibility = Visibility.Hidden;
                    }
                }
            }
            else
            {
                foreach (UIElement uiElement in ellipsisLoading.Container.Children)
                {
                    if (uiElement.Visibility == Visibility.Hidden)
                    {
                        uiElement.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        /// <summary>
        /// 隐藏时是否占据空间
        /// </summary>
        public bool IsHidePlaceholder
        {
            get { return (bool) GetValue(IsHidePlaceholderProperty); }
            set { SetValue(IsHidePlaceholderProperty, value); }
        }

        #endregion

        #region 构造

        public EllipsisLoading()
        {
            InitializeComponent();

            for (int i = 0; i < EllipsisCount; i++)
            {
                Container.Children.Add(
                    new TextBlock
                    {
                        Text = ".",
                        Visibility = IsHidePlaceholder ? Visibility.Hidden : Visibility.Collapsed
                    });
            }
        }

        #endregion
    }
}
