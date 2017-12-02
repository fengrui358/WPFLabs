using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EasingFunctionDemo.EasingFunctionConfigs
{
    public abstract class EasingFunctionConfig : IEasingFunctionConfig
    {
        public IEasingFunction ConfigEasingFunction { get; }
        public IEasingFunction RuningEasingFunction { get; }
        public UIElement ConfigUi { get; }
        public bool IsNew { get; }
        public void Config()
        {
            throw new NotImplementedException();
        }

        public void Confirm()
        {
            throw new NotImplementedException();
        }
    }
}
