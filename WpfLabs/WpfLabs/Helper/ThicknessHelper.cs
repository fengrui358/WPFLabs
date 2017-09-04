using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfLabs.Helper
{
    public static class ThicknessHelper
    {
        public static bool IsZero(Thickness thickness)
        {
            if (DoubleUtilHelper.IsZero(thickness.Left) && DoubleUtilHelper.IsZero(thickness.Top) && DoubleUtilHelper.IsZero(thickness.Right))
                return DoubleUtilHelper.IsZero(thickness.Bottom);
            return false;
        }

        public static bool IsUniform(Thickness thickness)
        {
            if (DoubleUtilHelper.AreClose(thickness.Left, thickness.Top) && DoubleUtilHelper.AreClose(thickness.Left, thickness.Right))
                return DoubleUtilHelper.AreClose(thickness.Left, thickness.Bottom);
            return false;
        }

        public static Size Size(Thickness thickness)
        {
            return new Size(thickness.Left + thickness.Right, thickness.Top + thickness.Bottom);
        }

        public static bool IsValid(Thickness thickness, bool allowNegative, bool allowNaN, bool allowPositiveInfinity,
            bool allowNegativeInfinity)
        {
            return (allowNegative || thickness.Left >= 0.0 && thickness.Right >= 0.0 &&
                    (thickness.Top >= 0.0 && thickness.Bottom >= 0.0)) &&
                   (allowNaN || !DoubleUtilHelper.IsNaN(thickness.Left) && !DoubleUtilHelper.IsNaN(thickness.Right) &&
                    (!DoubleUtilHelper.IsNaN(thickness.Top) && !DoubleUtilHelper.IsNaN(thickness.Bottom))) &&
                   ((allowPositiveInfinity || !double.IsPositiveInfinity(thickness.Left) &&
                     !double.IsPositiveInfinity(thickness.Right) &&
                     (!double.IsPositiveInfinity(thickness.Top) && !double.IsPositiveInfinity(thickness.Bottom))) &&
                    (allowNegativeInfinity || !double.IsNegativeInfinity(thickness.Left) &&
                     !double.IsNegativeInfinity(thickness.Right) &&
                     (!double.IsNegativeInfinity(thickness.Top) && !double.IsNegativeInfinity(thickness.Bottom))));
        }

        public static bool IsClose(Thickness thickness0, Thickness thickness1)
        {
            if (DoubleUtilHelper.AreClose(thickness0.Left, thickness1.Left) &&
                DoubleUtilHelper.AreClose(thickness0.Top, thickness1.Top) &&
                DoubleUtilHelper.AreClose(thickness0.Right, thickness1.Right))
                return DoubleUtilHelper.AreClose(thickness0.Bottom, thickness1.Bottom);
            return false;
        }
    }
}