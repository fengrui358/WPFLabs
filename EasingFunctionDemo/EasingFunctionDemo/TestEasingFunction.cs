using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EasingFunctionDemo
{
    public class TestEasingFunction : EasingFunctionBase
    {
        protected override Freezable CreateInstanceCore()
        {
            return (Freezable)new TestEasingFunction();
        }

        protected override double EaseInCore(double normalizedTime)
        {
            Debug.WriteLine(normalizedTime);
            return 1 - normalizedTime;
        }
    }
}
