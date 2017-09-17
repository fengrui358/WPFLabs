using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                if (Math.Abs(_drawingThickness - value) > Double.Epsilon && value > 0)
                {
                    _drawingThickness = value;
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
            var b = new Border();
        }

        private void Drawing()
        {
            var drawingBrush = new DrawingBrush();

            GeometryDrawing geometry = new GeometryDrawing();
            StreamGeometry streamGeometry = new StreamGeometry();

            var context = streamGeometry.Open();
            context.BeginFigure(new Point(0, 0), true, true);
            context.LineTo(new Point(_drawingWidth, 0), true, false);
            context.LineTo(new Point(_drawingWidth, _drawingHeight), true, false);
            context.LineTo(new Point(0, _drawingHeight), true, false);

            context.Close();

            geometry.Geometry = streamGeometry;
            geometry.Brush = new SolidColorBrush(Color.FromArgb(40, 155, 155, 155));
            geometry.Pen = new Pen(Brushes.Black, _drawingThickness);

            drawingBrush.Drawing = geometry;
            DrawingRectangle.Width = DrawingWidth;
            DrawingRectangle.Height = DrawingHeight;
            DrawingRectangle.Fill = drawingBrush;
        }
    }
}
