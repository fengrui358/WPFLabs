using System;
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
using System.Windows.Shapes;

namespace WpfLabs.ImagePerformanceDemo
{
    /// <summary>
    /// ImagePerformanceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ImagePerformanceWindow
    {
        public int Count { get; set; }

        public ImagePerformanceWindow()
        {
            Count = 3000;
            InitializeComponent();

            DataContext = this;
        }

        private void ShowWindow_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button) e.Source;
            switch (btn.Content.ToString().ToUpperInvariant())
            {
                case "PNG":
                    new PngImages(Count).ShowDialog();
                    break;
                case "SVG":
                    new SvgImages(Count).ShowDialog();
                    break;
                case "PATHIMAGES":
                    new PathImages(Count).ShowDialog();
                    break;
                case "FONT":
                    new FontImages(Count).ShowDialog();
                    break;
                case "PACKICONMATERIALS":
                    new PackIconMaterials(Count).ShowDialog();
                    break;
                case "PATH":
                    new PathImages(Count).ShowDialog();
                    break;
            }
        }
    }
}