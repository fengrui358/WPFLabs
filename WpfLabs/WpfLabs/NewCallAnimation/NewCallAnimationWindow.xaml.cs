using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfLabs.NewCallAnimation
{
    /// <summary>
    /// NewCallAnimationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewCallAnimationWindow : Window
    {
        private Random _random = new Random();
        private System.Threading.Timer _timer;

        public NewCallAnimationWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //RotateTimer.IsActive = !RotateTimer.IsActive;
        }

        private void NewCallAnimationWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _timer = new System.Threading.Timer(s =>
            {
                ControlBtn.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ControlBtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                }));

                _timer.Change(TimeSpan.FromSeconds(_random.Next(10, 21)), Timeout.InfiniteTimeSpan);
            }, null, TimeSpan.FromSeconds(_random.Next(10, 21)), Timeout.InfiniteTimeSpan);
        }

        private void NewCallAnimationWindow_OnUnloaded(object sender, RoutedEventArgs e)
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _timer = null;
        }
    }
}
