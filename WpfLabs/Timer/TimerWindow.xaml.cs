using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfLabs.Timer
{
    /// <summary>
    /// TimerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TimerWindow : Window, INotifyPropertyChanged
    {
        private bool _isStart;

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsStart
        {
            get { return _isStart; }
            private set
            {
                _isStart = value;
                OnPropertyChanged();
            }
        }

        public TimerWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void TimerWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                for (int i = 0; i < 100; i++)
                {
                    await Task.Delay(3000);
                    IsStart = true;

                    await Task.Delay(80 * 1000);
                    IsStart = false;
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
