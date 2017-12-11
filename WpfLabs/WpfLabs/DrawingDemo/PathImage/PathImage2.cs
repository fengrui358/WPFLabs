using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfLabs.DrawingDemo.PathImage
{
    public class PathImage2 : Image
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source),
            typeof(ImageSource), typeof(PathImage2),
            (PropertyMetadata) new FrameworkPropertyMetadata((object) null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(OnSourceChanged), (CoerceValueCallback) null),
            (ValidateValueCallback) null);

        public new ImageSource Source
        {
            get { return (ImageSource) this.GetValue(PathImage2.SourceProperty); }
            set { this.SetValue(PathImage2.SourceProperty, (object) value); }
        }

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(
            "Foreground", typeof(Brush), typeof(PathImage2),
            new FrameworkPropertyMetadata(default(Brush), OnForegroundChanged));

        /// <summary>
        /// Svg图片前景色
        /// </summary>
        public Brush Foreground
        {
            get { return (Brush) GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        /// <summary>
        /// 图片数据源变更
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pathImage = (PathImage2) d;

            var imageSource = e.NewValue as ImageSource;
            if (imageSource != null)
            {
                var drawingImage = imageSource as DrawingImage;
                if (drawingImage != null)
                {
                    var geometryDrawing = drawingImage.Drawing as GeometryDrawing;
                    if (geometryDrawing != null && !geometryDrawing.IsFrozen)
                    {
                        if (pathImage.Foreground != null)
                        {
                            geometryDrawing.Brush = pathImage.Foreground;
                        }
                    }
                }
            }

            var baseImage = (Image) d;
            baseImage.Source = imageSource;
        }

        /// <summary>
        /// 前景色改变
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pathImage = (PathImage2) d;

            var imageSource = pathImage.Source;
            if (imageSource != null)
            {
                var drawingImage = imageSource as DrawingImage;
                if (drawingImage != null)
                {
                    var geometryDrawing = drawingImage.Drawing as GeometryDrawing;
                    if (geometryDrawing != null && !geometryDrawing.IsFrozen)
                    {
                        if (pathImage.Foreground != null)
                        {
                            geometryDrawing.Brush = pathImage.Foreground;
                        }
                    }
                }
            }
        }
    }
}