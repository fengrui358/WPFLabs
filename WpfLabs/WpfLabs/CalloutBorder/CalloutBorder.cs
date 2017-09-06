using System;
using System.Collections.Generic;
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

        #endregion

        #region 依赖属性

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


        #region 私有方法

        private static void OnClearPenCache(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var border = (CalloutBorder) d;
            border._leftPenCache = (Pen)null;
            border._rightPenCache = (Pen)null;
            border._topPenCache = (Pen)null;
            border._bottomPenCache = (Pen)null;
        }

        private static bool IsThicknessValid(object value)
        {
            return ThicknessHelper.IsValid((Thickness) value, false, false, false, false);
        }

        private static bool IsCornerRadiusValid(object value)
        {
            return CornerRadiusHelper.IsValid((CornerRadius) value, false, false, false, false);
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

        #endregion
    }
}
