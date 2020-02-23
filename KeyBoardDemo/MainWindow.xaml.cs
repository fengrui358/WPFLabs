using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace KeyBoardDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<KeyEventArgs> ObservableCollection { get; }

        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection = new ObservableCollection<KeyEventArgs>();
            DataContext = this;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly != null)
            {
                var resourcesNames = assembly.GetManifestResourceNames();

                using (var keyStream =
                    assembly.GetManifestResourceStream(resourcesNames.First(s => s.EndsWith("Key.txt"))))
                {
                    using (var reader = new StreamReader(keyStream))
                    {
                        KeyMapper.Text = reader.ReadToEnd();
                    }
                }

                using (var keyStream =
                    assembly.GetManifestResourceStream(resourcesNames.First(s => s.EndsWith("ModifierKeys.txt"))))
                {
                    using (var reader = new StreamReader(keyStream))
                    {
                        ModifierKeysMapper.Text = reader.ReadToEnd();
                    }
                }
            }

            var sb = new StringBuilder();
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                sb.AppendLine($"{key}({KeyInterop.VirtualKeyFromKey(key)})");
            }

            VirtualKeys.Text = sb.ToString();
        }

        private void KeyBoardInputWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyTest.IsSelected)
            {
                ObservableCollection.Add(e);

                //尝试滚动到最下面
                Displayer.ScrollIntoView(ObservableCollection.Last());
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var w = new PreProcessInputTestWindow();
            w.Show();
        }
    }
}
