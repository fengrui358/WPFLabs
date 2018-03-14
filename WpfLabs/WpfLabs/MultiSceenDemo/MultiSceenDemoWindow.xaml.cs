using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace WpfLabs.MultiSceenDemo
{
    /// <summary>
    /// MultiSceenDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MultiSceenDemoWindow : INotifyPropertyChanged
    {
        private List<SystemParameterScreenPropertyInfo> _systemParameterScreenPropertyInfos;

        public List<SystemParameterScreenPropertyInfo> SystemParameterScreenPropertyInfos
        {
            get => _systemParameterScreenPropertyInfos;
            set
            {
                _systemParameterScreenPropertyInfos = value;
                OnPropertyChanged();
            }
        }

        private List<Screen> _screenInfos;

        public List<Screen> ScreenInfos
        {
            get => _screenInfos;
            set
            {
                _screenInfos = value;
                OnPropertyChanged();
            }
        }

        private Screen _selectedScreen;

        public Screen SelectedScreen
        {
            get => _selectedScreen;
            set
            {
                _selectedScreen = value;
                ReLocationWindow();
                OnPropertyChanged();
            }
        }

        public MultiSceenDemoWindow()
        {
            InitializeComponent();

            Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Refresh_OnClick(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            var systemParameterScreenPropertyInfos = new List<SystemParameterScreenPropertyInfo>();
            var properties = typeof(SystemParameters).GetProperties(BindingFlags.Static | BindingFlags.Public)
                .Where(s => s.Name.Contains("Screen") && !s.Name.Contains("Key"));

            foreach (var propertyInfo in properties)
            {
                systemParameterScreenPropertyInfos.Add(new SystemParameterScreenPropertyInfo(propertyInfo));
            }

            SystemParameterScreenPropertyInfos = systemParameterScreenPropertyInfos;

            ScreenInfos = Screen.AllScreens.ToList();
            SelectedScreen = Screen.PrimaryScreen;
        }

        private void ReLocationWindow()
        {
            WindowState = WindowState.Normal;

            Left = SelectedScreen.WorkingArea.Left;
            Top = SelectedScreen.WorkingArea.Top;
            Width = SelectedScreen.WorkingArea.Width;
            Height = SelectedScreen.WorkingArea.Height;

            WindowState = WindowState.Maximized;

            var selectedScreenProperties = typeof(Screen).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Labels.Children.Clear();
            Values.Children.Clear();

            foreach (var selectedScreenProperty in selectedScreenProperties)
            {
                Labels.Children.Add(new TextBlock { Text = selectedScreenProperty.Name });
                Values.Children.Add(new TextBlock
                {
                    Text = selectedScreenProperty.GetValue(SelectedScreen).ToString()
                });
            }
        }
    }
}
