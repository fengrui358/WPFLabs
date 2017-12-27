using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using MahApps.Metro.IconPacks;

namespace WpfLabs.ImagePerformanceDemo
{
    /// <summary>
    /// PackIconMaterials.xaml 的交互逻辑
    /// </summary>
    public partial class PackIconMaterials : Window
    {
        private Stopwatch _stopwatch;

        public PackIconMaterials(int count)
        {
            InitializeComponent();

            RecordStart();
            CreateImages(count);
        }

        private void CreateImages(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Container.Children.Add(new PackIconFontAwesome
                {
                    Kind = PackIconFontAwesomeKind.AccessibleIconBrands,
                    Foreground = Brushes.Red,
                    Width = 30,
                    Height = 30
                });
            }
        }

        private void RecordStart()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        private void Window_OnLoaded(object sender, RoutedEventArgs e)
        {
            _stopwatch.Stop();
            Msg.Text = $"耗时：{_stopwatch.ElapsedMilliseconds}ms";
        }
    }
}
