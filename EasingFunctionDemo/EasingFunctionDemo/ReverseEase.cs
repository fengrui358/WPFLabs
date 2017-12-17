using System.Windows;
using System.Windows.Media.Animation;

namespace EasingFunctionDemo
{
    public class ReverseEase : EasingFunctionBase
    {
        protected override Freezable CreateInstanceCore()
        {
            return new ReverseEase();
        }

        protected override double EaseInCore(double normalizedTime)
        {
            return 1 - normalizedTime;
        }
    }
}
