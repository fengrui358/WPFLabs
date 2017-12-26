using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
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

namespace WpfLabs.GenerateBitmapDemo
{
    /// <summary>
    /// GenerateBitmapDemo.xaml 的交互逻辑
    /// </summary>
    public partial class GenerateBitmapDemo : Window
    {
        private WriteableBitmap _writeableBitmap;

        public GenerateBitmapDemo()
        {
            InitializeComponent();
        }

        private void PaintBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dpi = GetDpi();
            _writeableBitmap = new WriteableBitmap((int)Img.Width, (int)Img.Height, dpi.Item1, dpi.Item2, PixelFormats.Bgra32, null);

            var color = new byte[] { 255, 0, 0, 255 };
            int stride = (_writeableBitmap.PixelWidth * _writeableBitmap.Format.BitsPerPixel) / 8;

            for (int i = 0; i < _writeableBitmap.PixelHeight; i++)
            {
                for (int j = 0; j < _writeableBitmap.PixelWidth; j++)
                {
                    var rect = new Int32Rect(i, j, 1, 1);
                    _writeableBitmap.WritePixels(rect, color, stride, 0);
                }
            }

            Img.Source = _writeableBitmap;
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_writeableBitmap != null)
            {
                RenderTargetBitmap rtbitmap = new RenderTargetBitmap(_writeableBitmap.PixelWidth,
                    _writeableBitmap.PixelHeight, _writeableBitmap.DpiX, _writeableBitmap.DpiY, PixelFormats.Default);
                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext context = drawingVisual.RenderOpen())
                {
                    context.DrawImage(_writeableBitmap, new Rect(0, 0, _writeableBitmap.Width, _writeableBitmap.Height));
                }

                rtbitmap.Render(drawingVisual);
                JpegBitmapEncoder bitmapEncoder = new JpegBitmapEncoder();
                bitmapEncoder.Frames.Add(BitmapFrame.Create(rtbitmap));

                var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Guid.NewGuid():N}.jpg");
                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
                {
                    bitmapEncoder.Save(fs);

                    //保存后自动打开
                    Process.Start(fileName);
                }
            }
        }

        /// <summary>
        /// 获取DPI值
        /// </summary>
        /// <returns></returns>
        private Tuple<double, double> GetDpi()
        {
            using (ManagementClass mc = new ManagementClass("Win32_DesktopMonitor"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {

                    double pixelsPerXLogicalInch = 0; // dpi for x
                    double pixelsPerYLogicalInch = 0; // dpi for y

                    foreach (var each in moc)
                    {
                        pixelsPerXLogicalInch = double.Parse((each.Properties["PixelsPerXLogicalInch"].Value.ToString()));
                        pixelsPerYLogicalInch = double.Parse((each.Properties["PixelsPerYLogicalInch"].Value.ToString()));
                    }
                    
                    return new Tuple<double, double>(pixelsPerXLogicalInch, pixelsPerYLogicalInch);
                }
            }
        }
    }
}
