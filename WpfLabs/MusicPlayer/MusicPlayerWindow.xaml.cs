using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
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
using Path = System.IO.Path;

namespace WpfLabs.MusicPlayer
{
    /// <summary>
    /// MusicPlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MusicPlayerWindow : Window, INotifyPropertyChanged
    {
        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        public MusicPlayerWindow()
        {
            InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MusicPlayerWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MusicPlayer", "TestFiles", "a1.mp3");
            //FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MusicPlayer", "TestFiles", "喜洋洋.wav");
        }
    }
}
