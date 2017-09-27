using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfLabs.Helper;

namespace WpfLabs.CalloutBorder
{
    public class CalloutBorder : Decorator
    {
        #region 字段

        private Pen _leftPenCache;
        private Pen _rightPenCache;
        private Pen _topPenCache;
        private Pen _bottomPenCache;
        private StreamGeometry _borderGeometryCache;
        private StreamGeometry _backgroundGeometryCache;
        private bool _useComplexRenderCodePath;
        private double _actualCalloutWidth;
        private double _actualCalloutHeight;
        private double? _actualHorizontalOffset;
        private double? _actualVerticalOffset;
        private bool _isShowCallout;
        private double _calloutThicknessHeight;
        private Radii? _radiiOuter;

        #endregion

        #region 依赖属性

        /// <summary>
        /// 标识箭头的位置
        /// </summary>
        public static readonly DependencyProperty PlacementProperty = DependencyProperty.Register("Placement", typeof(CalloutPlacement), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata(CalloutPlacement.Left, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(CalloutBorder.OnClearPenCache)));

        /// <summary>
        /// 标识箭头的宽
        /// </summary>
        public static readonly DependencyProperty CalloutWidthProperty = DependencyProperty.Register("CalloutWidth", typeof(double), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(CalloutBorder.OnClearPenCache)), new ValidateValueCallback(CalloutBorder.IsDoubleValid));

        /// <summary>
        /// 标识箭头的高
        /// </summary>
        public static readonly DependencyProperty CalloutHeightProperty = DependencyProperty.Register("CalloutHeight", typeof(double), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(CalloutBorder.OnClearPenCache)), new ValidateValueCallback(CalloutBorder.IsDoubleValid));

        /// <summary>
        /// 标识箭头的水平偏移
        /// </summary>
        public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(CalloutBorder.OnClearPenCache)), new ValidateValueCallback(CalloutBorder.IsDoubleValid));

        /// <summary>
        /// 标识箭头的垂直偏移
        /// </summary>
        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset", typeof(double), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(CalloutBorder.OnClearPenCache)), new ValidateValueCallback(CalloutBorder.IsDoubleValid));

        /// <summary>
        ///   标识 <see cref="P:System.Windows.Controls.Border.BorderThickness" /> 依赖属性。
        /// </summary>
        /// <returns>
        ///   <see cref="P:System.Windows.Controls.Border.BorderThickness" /> 依赖项属性的标识符。
        /// </returns>
        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(Thickness), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata((object)new Thickness(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(CalloutBorder.OnClearPenCache)), new ValidateValueCallback(CalloutBorder.IsThicknessValid));
        /// <summary>
        ///   标识 <see cref="P:System.Windows.Controls.Border.Padding" /> 依赖属性。
        /// </summary>
        /// <returns>
        ///   <see cref="P:System.Windows.Controls.Border.Padding" /> 依赖项属性的标识符。
        /// </returns>
        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof(Thickness), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata((object)new Thickness(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), new ValidateValueCallback(CalloutBorder.IsThicknessValid));
        /// <summary>
        ///   标识 <see cref="P:System.Windows.Controls.Border.CornerRadius" /> 依赖属性。
        /// </summary>
        /// <returns>
        ///   <see cref="P:System.Windows.Controls.Border.CornerRadius" /> 依赖项属性的标识符。
        /// </returns>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata((object)new CornerRadius(), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), new ValidateValueCallback(CalloutBorder.IsCornerRadiusValid));
        /// <summary>
        ///   标识 <see cref="P:System.Windows.Controls.Border.BorderBrush" /> 依赖属性。
        /// </summary>
        /// <returns>
        ///   <see cref="P:System.Windows.Controls.Border.BorderBrush" /> 依赖项属性的标识符。
        /// </returns>
        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender, new PropertyChangedCallback(CalloutBorder.OnClearPenCache)));
        /// <summary>
        ///   标识 <see cref="P:System.Windows.Controls.Border.Background" /> 依赖属性。
        /// </summary>
        /// <returns>
        ///   <see cref="P:System.Windows.Controls.Border.Background" /> 依赖项属性的标识符。
        /// </returns>
        public static readonly DependencyProperty BackgroundProperty = Panel.BackgroundProperty.AddOwner(typeof(CalloutBorder), (PropertyMetadata)new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        #endregion

        #region 属性

        /// <summary>
        /// 标识箭头的位置
        /// </summary>
        public CalloutPlacement Placement
        {
            get
            {
                return (CalloutPlacement)this.GetValue(CalloutBorder.PlacementProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.PlacementProperty, (object)value);
            }
        }

        /// <summary>
        /// 标识箭头的宽
        /// </summary>
        public double CalloutWidth
        {
            get
            {
                return (double)this.GetValue(CalloutBorder.CalloutWidthProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.CalloutWidthProperty, (object)value);
            }
        }

        /// <summary>
        /// 标识箭头的高
        /// </summary>
        public double CalloutHeight
        {
            get
            {
                return (double)this.GetValue(CalloutBorder.CalloutHeightProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.CalloutHeightProperty, (object)value);
            }
        }

        /// <summary>
        /// 标识箭头的水平偏移
        /// </summary>
        public double HorizontalOffset
        {
            get
            {
                return (double)this.GetValue(CalloutBorder.HorizontalOffsetProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.HorizontalOffsetProperty, (object)value);
            }
        }

        /// <summary>
        /// 标识箭头的垂直偏移
        /// </summary>
        public double VerticalOffset
        {
            get
            {
                return (double)this.GetValue(CalloutBorder.VerticalOffsetProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.VerticalOffsetProperty, (object)value);
            }
        }

        /// <summary>
        ///   获取或设置 <see cref="T:System.Windows.Controls.Border" /> 的相对 <see cref="T:System.Windows.Thickness" />。
        /// </summary>
        /// <returns>
        ///   描述 <see cref="T:System.Windows.Controls.Border" /> 的边界的宽度的 <see cref="T:System.Windows.Thickness" />。
        ///    此属性没有默认值。
        /// </returns>
        public Thickness BorderThickness
        {
            get
            {
                return (Thickness)this.GetValue(CalloutBorder.BorderThicknessProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.BorderThicknessProperty, (object)value);
            }
        }

        /// <summary>
        ///   获取或设置 <see cref="T:System.Windows.Thickness" /> 值，该值描述之间的空间量 <see cref="T:System.Windows.Controls.Border" /> 与其子元素。
        /// </summary>
        /// <returns>
        ///   <see cref="T:System.Windows.Thickness" /> 描述之间的空间量 <see cref="T:System.Windows.Controls.Border" /> 与单个子元素。
        ///    此属性没有默认值。
        /// </returns>
        public Thickness Padding
        {
            get
            {
                return (Thickness)this.GetValue(CalloutBorder.PaddingProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.PaddingProperty, (object)value);
            }
        }

        /// <summary>
        ///   获取或设置一个值，向其表示度的角变 <see cref="T:System.Windows.Controls.Border" /> 舍入。
        /// </summary>
        /// <returns>
        ///   <see cref="T:System.Windows.CornerRadius" /> 描述圆角的程度。
        ///    此属性没有默认值。
        /// </returns>
        public CornerRadius CornerRadius
        {
            get
            {
                return (CornerRadius)this.GetValue(CalloutBorder.CornerRadiusProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.CornerRadiusProperty, (object)value);
            }
        }

        /// <summary>
        ///   获取或设置用于绘制外部边框颜色的 <see cref="T:System.Windows.Media.Brush" />。
        /// </summary>
        /// <returns>
        ///   用于绘制外部边框颜色的 <see cref="T:System.Windows.Media.Brush" />。
        ///    此属性没有默认值。
        /// </returns>
        public Brush BorderBrush
        {
            get
            {
                return (Brush)this.GetValue(CalloutBorder.BorderBrushProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.BorderBrushProperty, (object)value);
            }
        }

        /// <summary>
        ///   获取或设置 <see cref="T:System.Windows.Media.Brush" />，它填充 <see cref="T:System.Windows.Controls.Border" /> 边界之间的区域。
        /// </summary>
        /// <returns>
        ///   绘制背景的 <see cref="T:System.Windows.Media.Brush" />。
        ///    此属性没有默认值。
        /// </returns>
        public Brush Background
        {
            get
            {
                return (Brush)this.GetValue(CalloutBorder.BackgroundProperty);
            }
            set
            {
                this.SetValue(CalloutBorder.BackgroundProperty, (object)value);
            }
        }

        #endregion

        #region 重载绘制

        /// <summary>
        ///   度量值的子元素 <see cref="T:System.Windows.Controls.Border" /> 过程中排列之前 <see cref="M:System.Windows.Controls.Border.ArrangeOverride(System.Windows.Size)" /> 传递。
        /// </summary>
        /// <param name="constraint">
        ///   上限 <see cref="T:System.Windows.Size" /> 不能超过限制。
        /// </param>
        /// <returns>
        ///   <see cref="T:System.Windows.Size" /> ，它表示该元素的大小限制。
        /// </returns>
        protected override Size MeasureOverride(Size constraint)
        {
            DebugWriterHelper.WriterLine($"Call MeasureOverride:{constraint}");

            UIElement child = this.Child;
            Size size1 = new Size();
            Thickness th = this.BorderThickness;

            //TODO:GetDPI
            //if (this.UseLayoutRounding && !FrameworkAppContextSwitches.DoNotApplyLayoutRoundingToMarginsAndBorderThickness)
            //{
            //    DpiScale dpi = this.GetDpi();
            //    th = new Thickness(UIElement.RoundLayoutValue(th.Left, dpi.DpiScaleX), UIElement.RoundLayoutValue(th.Top, dpi.DpiScaleY), UIElement.RoundLayoutValue(th.Right, dpi.DpiScaleX), UIElement.RoundLayoutValue(th.Bottom, dpi.DpiScaleY));
            //}

            //Thickness的宽和高
            Size size2 = CalloutBorder.HelperCollapseThickness(th);
            //Padding的宽和高
            Size size3 = CalloutBorder.HelperCollapseThickness(this.Padding);

            //测量小箭头的实际尺寸
            if (CalloutHeight > 0 && CalloutWidth > 0)
            {
                _radiiOuter = new CalloutBorder.Radii(CornerRadius, th, true);

                if (Placement == CalloutPlacement.Top || Placement == CalloutPlacement.Bottom)
                {
                    double residualWidth = 0d;
                    double residualHeight = 0d;

                    if (Placement == CalloutPlacement.Top)
                    {
                        residualWidth = constraint.Width - HorizontalOffset - _radiiOuter.Value.LeftTop - _radiiOuter.Value.RightTop;
                        _calloutThicknessHeight = Math.Sqrt(th.Top * th.Top * 2);
                        residualHeight = constraint.Height - _calloutThicknessHeight - th.Bottom;
                    }
                    else if(Placement == CalloutPlacement.Bottom)
                    {
                        residualWidth = constraint.Width - HorizontalOffset - _radiiOuter.Value.LeftBottom - _radiiOuter.Value.RightBottom;
                        _calloutThicknessHeight = Math.Sqrt(th.Bottom * th.Bottom * 2);
                        residualHeight = constraint.Height - _calloutThicknessHeight - th.Top;
                    }

                    if (residualWidth > 0 && residualHeight > 0)
                    {
                        _actualCalloutWidth = Math.Min(residualWidth, CalloutWidth);
                        _actualCalloutHeight = Math.Min(residualHeight, CalloutHeight);
                        _isShowCallout = true;
                    }
                }
                else
                {
                    double residualWidth = 0d;
                    double residualHeight = 0d;

                    if (Placement == CalloutPlacement.Left)
                    {
                        residualWidth = constraint.Height - VerticalOffset - _radiiOuter.Value.TopLeft - _radiiOuter.Value.BottomLeft;
                        _calloutThicknessHeight = Math.Sqrt(th.Left * th.Left * 2);
                        residualHeight = constraint.Width - _calloutThicknessHeight - th.Right;
                    }
                    else if (Placement == CalloutPlacement.Right)
                    {
                        residualWidth = constraint.Height - VerticalOffset - _radiiOuter.Value.TopRight - _radiiOuter.Value.BottomRight;
                        _calloutThicknessHeight = Math.Sqrt(th.Right * th.Right * 2);
                        residualHeight = constraint.Width - _calloutThicknessHeight - th.Left;
                    }

                    if (residualHeight > 0 && residualWidth > 0)
                    {
                        _actualCalloutHeight = Math.Min(residualHeight, CalloutHeight);
                        _actualCalloutWidth = Math.Min(residualWidth, CalloutWidth);
                        _isShowCallout = true;
                    }
                }
            }

            switch (Placement)
            {
                case CalloutPlacement.Top:
                    size1 = new Size(size2.Width + size3.Width,
                        size2.Height + size3.Height - th.Top + _actualCalloutHeight + _calloutThicknessHeight);
                    break;
                case CalloutPlacement.Bottom:
                    size1 = new Size(size2.Width + size3.Width,
                        size2.Height + size3.Height - th.Bottom + _actualCalloutHeight + _calloutThicknessHeight);
                    break;
                case CalloutPlacement.Left:
                    size1 =
                        new Size(
                            size2.Width + size3.Width - th.Left + _actualCalloutHeight + _calloutThicknessHeight,
                            size2.Height + size3.Height);
                    break;
                case CalloutPlacement.Right:
                    size1 =
                        new Size(
                            size2.Width + size3.Width - th.Left + _actualCalloutHeight + _calloutThicknessHeight,
                            size2.Height + size3.Height);
                    break;
            }

            if (child != null)
            {
                Size availableSize = new Size(Math.Max(0.0, constraint.Width - size1.Width),
                    Math.Max(0.0, constraint.Height - size1.Height));
                child.Measure(availableSize);
                Size desiredSize = child.DesiredSize;
                size1.Width = desiredSize.Width + size1.Width;
                size1.Height = desiredSize.Height + size1.Height;
            }

            return size1;
        }

        /// <summary>
        ///   排列的内容 <see cref="T:System.Windows.Controls.Border" /> 元素。
        /// </summary>
        /// <param name="finalSize">
        ///   <see cref="T:System.Windows.Size" /> 此元素用来排列其子元素。
        /// </param>
        /// <returns>
        ///   <see cref="T:System.Windows.Size" /> ，它表示的排列的大小 <see cref="T:System.Windows.Controls.Border" /> 元素与其子元素。
        /// </returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            DebugWriterHelper.WriterLine($"Call ArrangeOverride:{finalSize}");
            Thickness thickness = this.BorderThickness;

            //TODO:GetDPI
            //if (this.UseLayoutRounding && !FrameworkAppContextSwitches.DoNotApplyLayoutRoundingToMarginsAndBorderThickness)
            //{
            //    DpiScale dpi = this.GetDpi();
            //    thickness = new Thickness(UIElement.RoundLayoutValue(thickness.Left, dpi.DpiScaleX), UIElement.RoundLayoutValue(thickness.Top, dpi.DpiScaleY), UIElement.RoundLayoutValue(thickness.Right, dpi.DpiScaleX), UIElement.RoundLayoutValue(thickness.Bottom, dpi.DpiScaleY));
            //}

            Rect rect1 = new Rect(finalSize);
            Rect rect2 = CalloutBorder.HelperDeflateRect(rect1, thickness);
            if (_isShowCallout)
            {
                switch (Placement)
                {
                    case CalloutPlacement.Top:
                        rect2.Y = rect2.Y - thickness.Top + _actualCalloutHeight + _calloutThicknessHeight;
                        rect2.Height = Math.Max(0d,
                            rect2.Height + thickness.Top - _actualCalloutHeight - _calloutThicknessHeight);
                        break;
                    case CalloutPlacement.Bottom:
                        rect2.Height = Math.Max(0d,
                            rect2.Height + thickness.Bottom - _actualCalloutHeight - _calloutThicknessHeight);
                        break;
                    case CalloutPlacement.Left:
                        rect2.X = rect2.X - thickness.Left + _actualCalloutHeight + _calloutThicknessHeight;
                        rect2.Width = Math.Max(0d,
                            rect2.Width + thickness.Left - _actualCalloutHeight - _calloutThicknessHeight);
                        break;
                    case CalloutPlacement.Right:
                        rect2.Width = Math.Max(0d,
                            rect2.Width + thickness.Right - _actualCalloutHeight - _calloutThicknessHeight);
                        break;
                }
            }

            //重新布局子元素
            UIElement child = this.Child;
            if (child != null)
            {
                Rect finalRect = CalloutBorder.HelperDeflateRect(rect2, this.Padding);
                child.Arrange(finalRect);
            }
            CornerRadius cornerRadius = this.CornerRadius;
            Brush borderBrush = this.BorderBrush;
            this._useComplexRenderCodePath = !CalloutBorder.AreUniformCorners(cornerRadius);
            if (!this._useComplexRenderCodePath && borderBrush != null)
            {
                SolidColorBrush solidColorBrush = borderBrush as SolidColorBrush;
                bool isUniform = ThicknessHelper.IsUniform(thickness);
                this._useComplexRenderCodePath = solidColorBrush == null ||
                                                 (int) solidColorBrush.Color.A < (int) byte.MaxValue && !isUniform ||
                                                 !DoubleUtilHelper.IsZero(cornerRadius.TopLeft) && !isUniform;
            }
            if (this._useComplexRenderCodePath)
            {
                CalloutBorder.Radii radii1 = new CalloutBorder.Radii(cornerRadius, thickness, false);
                StreamGeometry streamGeometry1 = (StreamGeometry)null;
                if (!DoubleUtilHelper.IsZero(rect2.Width) && !DoubleUtilHelper.IsZero(rect2.Height))
                {
                    streamGeometry1 = new StreamGeometry();
                    using (StreamGeometryContext ctx = streamGeometry1.Open())
                        CalloutBorder.GenerateGeometry(ctx, rect2, radii1);
                    streamGeometry1.Freeze();
                    this._backgroundGeometryCache = streamGeometry1;
                }
                else
                    this._backgroundGeometryCache = (StreamGeometry)null;
                if (!DoubleUtilHelper.IsZero(rect1.Width) && !DoubleUtilHelper.IsZero(rect1.Height))
                {
                    if (_radiiOuter == null)
                    {
                        _radiiOuter = new CalloutBorder.Radii(cornerRadius, thickness, true);
                    }
                    StreamGeometry streamGeometry2 = new StreamGeometry();
                    using (StreamGeometryContext ctx = streamGeometry2.Open())
                    {
                        CalloutBorder.GenerateGeometry(ctx, rect1, _radiiOuter.Value);
                        if (streamGeometry1 != null)
                            CalloutBorder.GenerateGeometry(ctx, rect2, radii1);
                    }
                    streamGeometry2.Freeze();
                    this._borderGeometryCache = streamGeometry2;
                }
                else
                    this._borderGeometryCache = (StreamGeometry)null;
            }
            else
            {
                this._backgroundGeometryCache = (StreamGeometry)null;
                this._borderGeometryCache = (StreamGeometry)null;
            }
            return finalSize;
        }

        /// <summary>
        ///   在 <see cref="T:System.Windows.Controls.Border" /> 的呈现处理过程中，绘制 <see cref="T:System.Windows.Media.DrawingContext" /> 对象的内容。
        /// </summary>
        /// <param name="dc">
        ///   定义要绘制的对象的 <see cref="T:System.Windows.Media.DrawingContext" />。
        /// </param>
        protected override void OnRender(DrawingContext dc)
        {
            DebugWriterHelper.WriterLine("Call OnRender:");

            //TODO:GetDPI
            //bool useLayoutRounding = this.UseLayoutRounding;
            //DpiScale dpi = this.GetDpi();
            if (this._useComplexRenderCodePath)
            {
                StreamGeometry borderGeometryCache = this._borderGeometryCache;
                Brush borderBrush;
                if (borderGeometryCache != null && (borderBrush = this.BorderBrush) != null)
                    dc.DrawGeometry(borderBrush, (Pen)null, (Geometry)borderGeometryCache);
                StreamGeometry backgroundGeometryCache = this._backgroundGeometryCache;
                Brush background;
                if (backgroundGeometryCache == null || (background = this.Background) == null)
                    return;
                dc.DrawGeometry(background, (Pen)null, (Geometry)backgroundGeometryCache);
            }
            else
            {
                Thickness borderThickness = this.BorderThickness;
                CornerRadius cornerRadius = this.CornerRadius;
                double topLeft1 = cornerRadius.TopLeft;

                //flag:左上角半径不为0
                bool flag = !DoubleUtilHelper.IsZero(topLeft1);
                Brush borderBrush;
                Size renderSize;
                if (!ThicknessHelper.IsZero(borderThickness) && (borderBrush = this.BorderBrush) != null)
                {
                    Pen pen1 = this._leftPenCache;
                    if (pen1 == null)
                    {
                        pen1 = new Pen();
                        pen1.Brush = borderBrush;
                        //TODO:GetDPI
                        //pen1.Thickness = !useLayoutRounding ? borderThickness.Left : UIElement.RoundLayoutValue(borderThickness.Left, dpi.DpiScaleX);
                        pen1.Thickness = borderThickness.Left;
                        if (borderBrush.IsFrozen)
                            pen1.Freeze();
                        this._leftPenCache = pen1;
                    }
                    if (ThicknessHelper.IsUniform(borderThickness))
                    {
                        double num = pen1.Thickness * 0.5;
                        Rect rectangle = new Rect(new Point(num, num), new Point(this.RenderSize.Width - num, this.RenderSize.Height - num));
                        if (flag)
                            dc.DrawRoundedRectangle((Brush)null, pen1, rectangle, topLeft1, topLeft1);
                        else
                            dc.DrawRectangle((Brush)null, pen1, rectangle);
                    }
                    else
                    {
                        if (DoubleUtilHelper.GreaterThan(borderThickness.Left, 0.0))
                        {
                            double x1 = pen1.Thickness * 0.5;
                            DrawingContext drawingContext = dc;
                            Pen pen2 = pen1;
                            Point point0 = new Point(x1, 0.0);
                            double x2 = x1;
                            renderSize = this.RenderSize;
                            double height = renderSize.Height;
                            Point point1 = new Point(x2, height);
                            drawingContext.DrawLine(pen2, point0, point1);
                        }
                        if (DoubleUtilHelper.GreaterThan(borderThickness.Right, 0.0))
                        {
                            Pen pen2 = this._rightPenCache;
                            if (pen2 == null)
                            {
                                pen2 = new Pen();
                                pen2.Brush = borderBrush;
                                //TODO:GetDPI
                                //pen2.Thickness = !useLayoutRounding ? borderThickness.Right : UIElement.RoundLayoutValue(borderThickness.Right, dpi.DpiScaleX);
                                pen2.Thickness = borderThickness.Right;
                                if (borderBrush.IsFrozen)
                                    pen2.Freeze();
                                this._rightPenCache = pen2;
                            }
                            double num = pen2.Thickness * 0.5;
                            DrawingContext drawingContext = dc;
                            Pen pen3 = pen2;
                            renderSize = this.RenderSize;
                            Point point0 = new Point(renderSize.Width - num, 0.0);
                            renderSize = this.RenderSize;
                            double x = renderSize.Width - num;
                            renderSize = this.RenderSize;
                            double height = renderSize.Height;
                            Point point1 = new Point(x, height);
                            drawingContext.DrawLine(pen3, point0, point1);
                        }
                        if (DoubleUtilHelper.GreaterThan(borderThickness.Top, 0.0))
                        {
                            Pen pen2 = this._topPenCache;
                            if (pen2 == null)
                            {
                                pen2 = new Pen();
                                pen2.Brush = borderBrush;
                                //TODO:GetDPI
                                //pen2.Thickness = !useLayoutRounding ? borderThickness.Top : UIElement.RoundLayoutValue(borderThickness.Top, dpi.DpiScaleY);
                                pen2.Thickness = borderThickness.Top;
                                if (borderBrush.IsFrozen)
                                    pen2.Freeze();
                                this._topPenCache = pen2;
                            }
                            double y = pen2.Thickness * 0.5;
                            DrawingContext drawingContext = dc;
                            Pen pen3 = pen2;
                            Point point0 = new Point(0.0, y);
                            renderSize = this.RenderSize;
                            Point point1 = new Point(renderSize.Width, y);
                            drawingContext.DrawLine(pen3, point0, point1);
                        }
                        if (DoubleUtilHelper.GreaterThan(borderThickness.Bottom, 0.0))
                        {
                            Pen pen2 = this._bottomPenCache;
                            if (pen2 == null)
                            {
                                pen2 = new Pen();
                                pen2.Brush = borderBrush;
                                //TODO:GetDPI
                                //pen2.Thickness = !useLayoutRounding ? borderThickness.Bottom : UIElement.RoundLayoutValue(borderThickness.Bottom, dpi.DpiScaleY);
                                pen2.Thickness = borderThickness.Bottom;
                                if (borderBrush.IsFrozen)
                                    pen2.Freeze();
                                this._bottomPenCache = pen2;
                            }
                            double num = pen2.Thickness * 0.5;
                            DrawingContext drawingContext = dc;
                            Pen pen3 = pen2;
                            double x = 0.0;
                            renderSize = this.RenderSize;
                            double y1 = renderSize.Height - num;
                            Point point0 = new Point(x, y1);
                            renderSize = this.RenderSize;
                            double width = renderSize.Width;
                            renderSize = this.RenderSize;
                            double y2 = renderSize.Height - num;
                            Point point1 = new Point(width, y2);
                            drawingContext.DrawLine(pen3, point0, point1);
                        }
                    }
                }
                Brush background = this.Background;
                if (background == null)
                    return;
                Point point1_1;
                Point point2;
                //TODO:GetDPI
                //if (useLayoutRounding)
                //{
                //    point1_1 = new Point(UIElement.RoundLayoutValue(borderThickness.Left, dpi.DpiScaleX), UIElement.RoundLayoutValue(borderThickness.Top, dpi.DpiScaleY));
                //    if (FrameworkAppContextSwitches.DoNotApplyLayoutRoundingToMarginsAndBorderThickness)
                //    {
                //        // ISSUE: explicit reference operation
                //        // ISSUE: variable of a reference type
                //        Point & local = @point2;
                //        renderSize = this.RenderSize;
                //        double x = UIElement.RoundLayoutValue(renderSize.Width - borderThickness.Right, dpi.DpiScaleX);
                //        renderSize = this.RenderSize;
                //        double y = UIElement.RoundLayoutValue(renderSize.Height - borderThickness.Bottom, dpi.DpiScaleY);
                //            // ISSUE: explicit reference operation
                //            ^ local = new Point(x, y);
                //    }
                //    else
                //    {
                //        // ISSUE: explicit reference operation
                //        // ISSUE: variable of a reference type
                //        Point & local = @point2;
                //        renderSize = this.RenderSize;
                //        double x = renderSize.Width - UIElement.RoundLayoutValue(borderThickness.Right, dpi.DpiScaleX);
                //        renderSize = this.RenderSize;
                //        double y = renderSize.Height - UIElement.RoundLayoutValue(borderThickness.Bottom, dpi.DpiScaleY);
                //            // ISSUE: explicit reference operation
                //            ^ local = new Point(x, y);
                //    }
                //}
                //else
                //{

                if (!_isShowCallout)
                {
                    point1_1 = new Point(borderThickness.Left, borderThickness.Top);
                    // ISSUE: explicit reference operation
                    // ISSUE: variable of a reference type

                    renderSize = this.RenderSize;
                    double px2 = renderSize.Width - borderThickness.Right;
                    double py2 = renderSize.Height - borderThickness.Bottom;
                    // ISSUE: explicit reference operation
                    point2 = new Point(px2, py2);

                    //}
                    if (point2.X <= point1_1.X || point2.Y <= point1_1.Y)
                        return;
                    if (flag)
                    {
                        double topLeft2 = new CalloutBorder.Radii(cornerRadius, borderThickness, false).TopLeft;
                        dc.DrawRoundedRectangle(background, (Pen)null, new Rect(point1_1, point2), topLeft2, topLeft2);
                    }
                    else
                    {
                        dc.DrawRectangle(background, (Pen)null, new Rect(point1_1, point2));
                    }
                }
                else
                {
                    switch (Placement)
                    {
                        case CalloutPlacement.Top:
                            point1_1 = new Point(_radiiOuter.Value.LeftTop, _actualHorizontalOffset);

                            break;
                        case CalloutPlacement.Bottom:
                            break;
                        case CalloutPlacement.Left:
                            break;
                        case CalloutPlacement.Right:
                            break;
                    }
                }
            }
        }

        #endregion

        #region 私有方法

        private static void OnClearPenCache(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var border = (CalloutBorder) d;
            border._leftPenCache = (Pen)null;
            border._rightPenCache = (Pen)null;
            border._topPenCache = (Pen)null;
            border._bottomPenCache = (Pen)null;
            border._actualCalloutWidth = 0d;
            border._actualCalloutHeight = 0d;
            border._actualHorizontalOffset = null;
            border._actualVerticalOffset = null;
            border._isShowCallout = false;
        }

        private static bool IsDoubleValid(object value)
        {
            return (double) value >= 0d;
        }

        private static bool IsThicknessValid(object value)
        {
            return ThicknessHelper.IsValid((Thickness) value, false, false, false, false);
        }

        private static bool IsCornerRadiusValid(object value)
        {
            return CornerRadiusHelper.IsValid((CornerRadius) value, false, false, false, false);
        }

        private static Size HelperCollapseThickness(Thickness th)
        {
            return new Size(th.Left + th.Right, th.Top + th.Bottom);
        }

        private static bool AreUniformCorners(CornerRadius borderRadii)
        {
            double topLeft = borderRadii.TopLeft;
            if (DoubleUtilHelper.AreClose(topLeft, borderRadii.TopRight) && DoubleUtilHelper.AreClose(topLeft, borderRadii.BottomLeft))
                return DoubleUtilHelper.AreClose(topLeft, borderRadii.BottomRight);
            return false;
        }

        private static Rect HelperDeflateRect(Rect rt, Thickness thick)
        {
            return new Rect(rt.Left + thick.Left, rt.Top + thick.Top, Math.Max(0.0, rt.Width - thick.Left - thick.Right), Math.Max(0.0, rt.Height - thick.Top - thick.Bottom));
        }

        private static void GenerateGeometry(StreamGeometryContext ctx, Rect rect, CalloutBorder.Radii radii)
        {
            Point point1 = new Point(radii.LeftTop, 0.0);
            Point point2 = new Point(rect.Width - radii.RightTop, 0.0);
            Point point3 = new Point(rect.Width, radii.TopRight);
            Point point4 = new Point(rect.Width, rect.Height - radii.BottomRight);
            Point point5 = new Point(rect.Width - radii.RightBottom, rect.Height);
            Point point6 = new Point(radii.LeftBottom, rect.Height);
            Point point7 = new Point(0.0, rect.Height - radii.BottomLeft);
            Point point8 = new Point(0.0, radii.TopLeft);
            if (point1.X > point2.X)
            {
                double num = radii.LeftTop / (radii.LeftTop + radii.RightTop) * rect.Width;
                point1.X = num;
                point2.X = num;
            }
            if (point3.Y > point4.Y)
            {
                double num = radii.TopRight / (radii.TopRight + radii.BottomRight) * rect.Height;
                point3.Y = num;
                point4.Y = num;
            }
            if (point5.X < point6.X)
            {
                double num = radii.LeftBottom / (radii.LeftBottom + radii.RightBottom) * rect.Width;
                point5.X = num;
                point6.X = num;
            }
            if (point7.Y < point8.Y)
            {
                double num = radii.TopLeft / (radii.TopLeft + radii.BottomLeft) * rect.Height;
                point7.Y = num;
                point8.Y = num;
            }
            // ISSUE: explicit reference operation
            // ISSUE: variable of a reference type
            Point point9 = rect.TopLeft;
            double x1 = point9.X;
            point9 = rect.TopLeft;
            double y1 = point9.Y;
            // ISSUE: explicit reference operation
            Vector vector = new Vector(x1, y1);
            point1 += vector;
            point2 += vector;
            point3 += vector;
            point4 += vector;
            point5 += vector;
            point6 += vector;
            point7 += vector;
            point8 += vector;
            ctx.BeginFigure(point1, true, true);
            ctx.LineTo(point2, true, false);
            point9 = rect.TopRight;
            double width1 = point9.X - point2.X;
            double y2 = point3.Y;
            point9 = rect.TopRight;
            double y3 = point9.Y;
            double height1 = y2 - y3;
            if (!DoubleUtilHelper.IsZero(width1) || !DoubleUtilHelper.IsZero(height1))
                ctx.ArcTo(point3, new Size(width1, height1), 0.0, false, SweepDirection.Clockwise, true, false);
            ctx.LineTo(point4, true, false);
            point9 = rect.BottomRight;
            double width2 = point9.X - point5.X;
            point9 = rect.BottomRight;
            double height2 = point9.Y - point4.Y;
            if (!DoubleUtilHelper.IsZero(width2) || !DoubleUtilHelper.IsZero(height2))
                ctx.ArcTo(point5, new Size(width2, height2), 0.0, false, SweepDirection.Clockwise, true, false);
            ctx.LineTo(point6, true, false);
            double x2 = point6.X;
            point9 = rect.BottomLeft;
            double x3 = point9.X;
            double width3 = x2 - x3;
            point9 = rect.BottomLeft;
            double height3 = point9.Y - point7.Y;
            if (!DoubleUtilHelper.IsZero(width3) || !DoubleUtilHelper.IsZero(height3))
                ctx.ArcTo(point7, new Size(width3, height3), 0.0, false, SweepDirection.Clockwise, true, false);
            ctx.LineTo(point8, true, false);
            double x4 = point1.X;
            point9 = rect.TopLeft;
            double x5 = point9.X;
            double width4 = x4 - x5;
            double y4 = point8.Y;
            point9 = rect.TopLeft;
            double y5 = point9.Y;
            double height4 = y4 - y5;
            if (DoubleUtilHelper.IsZero(width4) && DoubleUtilHelper.IsZero(height4))
                return;
            ctx.ArcTo(point1, new Size(width4, height4), 0.0, false, SweepDirection.Clockwise, true, false);
        }

        #endregion

        #region 内部类

        private struct Radii
        {
            internal double LeftTop;
            internal double TopLeft;
            internal double TopRight;
            internal double RightTop;
            internal double RightBottom;
            internal double BottomRight;
            internal double BottomLeft;
            internal double LeftBottom;

            internal Radii(CornerRadius radii, Thickness borders, bool outer)
            {
                double num1 = 0.5 * borders.Left;
                double num2 = 0.5 * borders.Top;
                double num3 = 0.5 * borders.Right;
                double num4 = 0.5 * borders.Bottom;
                if (outer)
                {
                    if (DoubleUtilHelper.IsZero(radii.TopLeft))
                    {
                        this.LeftTop = this.TopLeft = 0.0;
                    }
                    else
                    {
                        this.LeftTop = radii.TopLeft + num1;
                        this.TopLeft = radii.TopLeft + num2;
                    }
                    if (DoubleUtilHelper.IsZero(radii.TopRight))
                    {
                        this.TopRight = this.RightTop = 0.0;
                    }
                    else
                    {
                        this.TopRight = radii.TopRight + num2;
                        this.RightTop = radii.TopRight + num3;
                    }
                    if (DoubleUtilHelper.IsZero(radii.BottomRight))
                    {
                        this.RightBottom = this.BottomRight = 0.0;
                    }
                    else
                    {
                        this.RightBottom = radii.BottomRight + num3;
                        this.BottomRight = radii.BottomRight + num4;
                    }
                    if (DoubleUtilHelper.IsZero(radii.BottomLeft))
                    {
                        this.BottomLeft = this.LeftBottom = 0.0;
                    }
                    else
                    {
                        this.BottomLeft = radii.BottomLeft + num4;
                        this.LeftBottom = radii.BottomLeft + num1;
                    }
                }
                else
                {
                    this.LeftTop = Math.Max(0.0, radii.TopLeft - num1);
                    this.TopLeft = Math.Max(0.0, radii.TopLeft - num2);
                    this.TopRight = Math.Max(0.0, radii.TopRight - num2);
                    this.RightTop = Math.Max(0.0, radii.TopRight - num3);
                    this.RightBottom = Math.Max(0.0, radii.BottomRight - num3);
                    this.BottomRight = Math.Max(0.0, radii.BottomRight - num4);
                    this.BottomLeft = Math.Max(0.0, radii.BottomLeft - num4);
                    this.LeftBottom = Math.Max(0.0, radii.BottomLeft - num1);
                }
            }
        }

        /// <summary>
        /// 符号位置
        /// </summary>
        public enum CalloutPlacement
        {
            /// <summary>
            /// 左
            /// </summary>
            Left,

            /// <summary>
            /// 上
            /// </summary>
            Top,

            /// <summary>
            /// 右
            /// </summary>
            Right,
            /// <summary>
            /// 底
            /// </summary>
            Bottom
        }

        #endregion
    }
}
