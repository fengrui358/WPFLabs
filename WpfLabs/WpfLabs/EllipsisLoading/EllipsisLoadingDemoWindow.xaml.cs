using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace WpfLabs.EllipsisLoading
{
    /// <summary>
    /// EllipsisLoadingDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EllipsisLoadingDemoWindow : INotifyPropertyChanged
    {
        private bool _isStart;

        public bool IsStart
        {
            get { return _isStart; }
            set
            {
                _isStart = value;
                OnPropertyChanged();
            }
        }

        public EllipsisLoadingDemoWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void IsActiveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                IsStart = !IsStart;
            });
        }
    }
}
