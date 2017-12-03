using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace EasingFunctionDemo.EasingFunctionConfigs
{
    public class SplineKeyFrameConfigModel : ObservableObject
    {
        private double _controlPoint1X;

        public double ControlPoint1X
        {
            get => _controlPoint1X;
            set
            {
                if (value >= 0 && value <= 1)
                {
                    Set(ref _controlPoint1X, value);
                }
            }
        }

        private double _controlPoint1Y;

        public double ControlPoint1Y
        {
            get => _controlPoint1Y;
            set
            {
                if (value >= 0 && value <= 1)
                {
                    Set(ref _controlPoint1Y, value);
                }
            }
        }

        private double _controlPoint2X = 1;

        public double ControlPoint2X
        {
            get => _controlPoint2X;
            set
            {
                if (value >= 0 && value <= 1)
                {
                    Set(ref _controlPoint2X, value);
                }
            }
        }

        private double _controlPoint2Y;

        public double ControlPoint2Y
        {
            get => _controlPoint2Y;
            set
            {
                if (value >= 0 && value <= 1)
                {
                    Set(ref _controlPoint2Y, value);
                }
            }
        }
    }
}
