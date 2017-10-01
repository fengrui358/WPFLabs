using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace WpfLabs.DrawingDemo
{
    /// <summary>
    /// DrawingDemoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DrawingDemoWindow : Window
    {
        private double _drawingWidth = 150;
        private double _drawingHeight = 100;
        private double _drawingThickness = 1;
        private double _radii = 0;
        private string _extName;

        public double DrawingWidth
        {
            get => _drawingWidth;
            set
            {
                if (Math.Abs(_drawingWidth - value) > double.Epsilon && value > 0)
                {
                    _drawingWidth = value;
                    Drawing();
                }
            }
        }

        public double DrawingHeight
        {
            get => _drawingHeight;
            set
            {
                if (Math.Abs(_drawingHeight - value) > Double.Epsilon && value > 0)
                {
                    _drawingHeight = value;
                    Drawing();
                }
            }
        }

        public double DrawingThickness
        {
            get => _drawingThickness;
            set
            {
                if (Math.Abs(_drawingThickness - value) > Double.Epsilon && value >= 0)
                {
                    _drawingThickness = value;
                    Drawing();
                }
            }
        }

        public double Radii
        {
            get => _radii;
            set
            {
                if (Math.Abs(_radii - value) > Double.Epsilon && value >= 0)
                {
                    _radii = value;
                    Drawing();
                }
            }
        }

        public DrawingDemoWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void DrawingDemoWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Drawing();
        }

        private void Drawing()
        {
            StreamGeometry streamGeometry = new StreamGeometry();

            using (var context = streamGeometry.Open())
            {
                context.BeginFigure(new Point(Radii, 0), true, true);
                context.LineTo(new Point(_drawingWidth - Radii, 0), true, false);
                context.ArcTo(new Point(_drawingWidth, Radii), new Size(Radii, Radii), 0, false,
                    SweepDirection.Clockwise, true, false);
                context.LineTo(new Point(_drawingWidth, _drawingHeight - Radii), true, false);
                context.ArcTo(new Point(_drawingWidth - Radii, _drawingHeight), new Size(Radii, Radii), 0, false,
                    SweepDirection.Clockwise, true, false);
                context.LineTo(new Point(Radii, _drawingHeight), true, false);
                context.ArcTo(new Point(0, _drawingHeight - Radii), new Size(Radii, Radii), 0, false,
                    SweepDirection.Clockwise, true, false);
                context.LineTo(new Point(0, Radii), true, false);
                context.ArcTo(new Point(Radii, 0), new Size(Radii, Radii), 0, false,
                    SweepDirection.Clockwise, true, false);
            }

            DrawingPath.StrokeThickness = DrawingThickness;
            DrawingPath.Stroke = new SolidColorBrush(Colors.Blue);
            DrawingPath.Fill = new SolidColorBrush(Color.FromArgb(30,255,0,0));
            DrawingPath.Data = streamGeometry;
        }

        /// <summary>
        /// 保存截图
        /// </summary>
        /// <param name="ui">控件名称</param>
        /// <param name="filename">图片文件名</param>
        public void SaveFrameworkElementToImage(FrameworkElement ui, string filename)
        {
            try
            {
                var ms = new FileStream(filename, FileMode.Create);
                var bmp = new RenderTargetBitmap((int)ui.ActualWidth, (int)ui.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
                bmp.Render(ui);
                
                //var bmp = RenderVisual(ui);

                var ext = Path.GetExtension(filename).ToUpper();
                BitmapEncoder encoder;
                switch (ext)
                {
                    case ".JPG":
                        encoder = new JpegBitmapEncoder();
                        break;
                    case ".PNG":
                        encoder = new PngBitmapEncoder();
                        break;
                    case ".BMP":
                        encoder = new BmpBitmapEncoder();
                        break;
                    default:
                        encoder = new BmpBitmapEncoder();
                        break;
                }

                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(ms);
                ms.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 后缀RadioButton切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtRadioButton_OnChecked(object sender, RoutedEventArgs e)
        {
            _extName = ((RadioButton) sender).Content.ToString();
        }

        /// <summary>
        /// 截屏并打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnapButton_OnClick(object sender, RoutedEventArgs e)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Guid.NewGuid():N}.{_extName}");

            SaveFrameworkElementToImage(DrawingContainer, filePath);
            Process.Start(filePath);
        }
    }
}
