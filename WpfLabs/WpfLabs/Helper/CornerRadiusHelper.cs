using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfLabs.Helper
{
    public static class CornerRadiusHelper
    {
        public static bool IsValid(CornerRadius cornerRadius, bool allowNegative, bool allowNaN,
            bool allowPositiveInfinity, bool allowNegativeInfinity)
        {
            return (allowNegative || cornerRadius.TopLeft >= 0.0 && cornerRadius.TopRight >= 0.0 &&
                    (cornerRadius.BottomLeft >= 0.0 && cornerRadius.BottomRight >= 0.0)) &&
                   (allowNaN || !DoubleUtilHelper.IsNaN(cornerRadius.TopLeft) && !DoubleUtilHelper.IsNaN(cornerRadius.TopRight) &&
                    (!DoubleUtilHelper.IsNaN(cornerRadius.BottomLeft) && !DoubleUtilHelper.IsNaN(cornerRadius.BottomRight))) &&
                   ((allowPositiveInfinity || !double.IsPositiveInfinity(cornerRadius.TopLeft) &&
                     !double.IsPositiveInfinity(cornerRadius.TopRight) &&
                     (!double.IsPositiveInfinity(cornerRadius.BottomLeft) && !double.IsPositiveInfinity(cornerRadius.BottomRight))) &&
                    (allowNegativeInfinity || !double.IsNegativeInfinity(cornerRadius.TopLeft) &&
                     !double.IsNegativeInfinity(cornerRadius.TopRight) &&
                     (!double.IsNegativeInfinity(cornerRadius.BottomLeft) && !double.IsNegativeInfinity(cornerRadius.BottomRight))));
        }
    }
}
