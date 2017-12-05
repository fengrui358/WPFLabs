using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MS.Internal.PresentationFramework;

namespace WpfLabs.DrawingDemo.PathImage
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.DrawingDemo"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfLabs.DrawingDemo;assembly=WpfLabs.DrawingDemo"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:PathImage/>
    ///
    /// </summary>
    public class PathImage : Control
    {
        private ImageSource _imageSource;
        private Image _image;

        /// <summary>Identifies the <see cref="P:System.Windows.Controls.Image.Source" /> dependency property. </summary>
        /// <returns>The identifier for the <see cref="P:System.Windows.Controls.Image.Source" /> dependency property.</returns>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source",
            typeof(ImageSource), typeof(PathImage),
            (PropertyMetadata) new FrameworkPropertyMetadata((object) null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                OnSourceChanged, (CoerceValueCallback) null), (ValidateValueCallback) null);

        /// <summary>Identifies the <see cref="P:System.Windows.Controls.Image.Stretch" /> dependency property. </summary>
        /// <returns>The identifier for the <see cref="P:System.Windows.Controls.Image.Stretch" /> dependency property.</returns>
        public static readonly DependencyProperty StretchProperty = Viewbox.StretchProperty.AddOwner(typeof(PathImage));

        /// <summary>Identifies the <see cref="T:System.Windows.Controls.StretchDirection" /> dependency property. </summary>
        /// <returns>The identifier for the <see cref="T:System.Windows.Controls.StretchDirection" /> dependency property.</returns>
        public static readonly DependencyProperty StretchDirectionProperty = Viewbox.StretchDirectionProperty.AddOwner(typeof(PathImage));

        /// <summary>Identifies the <see cref="E:System.Windows.Controls.Image.ImageFailed" /> routed event.</summary>
        /// <returns>The identifier for the <see cref="E:System.Windows.Controls.Image.ImageFailed" /> routed event.</returns>
        public static readonly RoutedEvent ImageFailedEvent = Image.ImageFailedEvent.AddOwner(typeof(PathImage));

        /// <summary>Gets or sets the <see cref="T:System.Windows.Media.ImageSource" /> for the image.  </summary>
        /// <returns>The source of the drawn image. The default value is <see langword="null" />.</returns>
        public ImageSource Source
        {
            get
            {
                return (ImageSource)this.GetValue(SourceProperty);
            }
            set
            {
                this.SetValue(SourceProperty, (object)value);
            }
        }

        /// <summary>Gets or sets a value that describes how an <see cref="T:System.Windows.Controls.Image" /> should be stretched to fill the destination rectangle.  </summary>
        /// <returns>One of the <see cref="T:System.Windows.Media.Stretch" /> values. The default is <see cref="F:System.Windows.Media.Stretch.Uniform" />.</returns>
        public Stretch Stretch
        {
            get
            {
                return (Stretch)this.GetValue(PathImage.StretchProperty);
            }
            set
            {
                this.SetValue(PathImage.StretchProperty, (object)value);
            }
        }

        /// <summary>Gets or sets a value that indicates how the image is scaled.  </summary>
        /// <returns>One of the <see cref="T:System.Windows.Controls.StretchDirection" /> values. The default is <see cref="F:System.Windows.Controls.StretchDirection.Both" />.</returns>
        public StretchDirection StretchDirection
        {
            get
            {
                return (StretchDirection)this.GetValue(PathImage.StretchDirectionProperty);
            }
            set
            {
                this.SetValue(PathImage.StretchDirectionProperty, (object)value);
            }
        }

        ///// <summary>Occurs when there is a failure in the image.</summary>
        //public event EventHandler<ExceptionRoutedEventArgs> ImageFailed
        //{
        //    add
        //    {
        //        this.AddHandler(Image.ImageFailedEvent, (Delegate)value);
        //    }
        //    remove
        //    {
        //        this.RemoveHandler(Image.ImageFailedEvent, (Delegate)value);
        //    }
        //}

        static PathImage()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathImage), new FrameworkPropertyMetadata(typeof(PathImage)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _image = GetTemplateChild("PART_Img") as Image;
            if (_image != null && _image.Source != _imageSource)
            {
                _image.Source = _imageSource;
            }
        }

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pathImage = (PathImage) d;

            pathImage._imageSource = e.NewValue as ImageSource;

            if (pathImage._image != null)
            {
                pathImage._image.Source = pathImage._imageSource;
            }
        }
    }
}
