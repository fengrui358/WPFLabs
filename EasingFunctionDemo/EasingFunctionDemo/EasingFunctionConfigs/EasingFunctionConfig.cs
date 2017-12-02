using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using GalaSoft.MvvmLight;

namespace EasingFunctionDemo.EasingFunctionConfigs
{
    public class EasingFunctionConfig : ObservableObject, IEasingFunctionConfig
    {
        private IEasingFunction _configEasingFunction;

        public IEasingFunction ConfigEasingFunction
        {
            get { return _configEasingFunction; }
            private set
            {
                if (_configEasingFunction != value)
                {
                    _configEasingFunction = value;
                    ConfigUi = new EasingFunctionConfigUi((EasingFunctionBase) _configEasingFunction);
                    RaisePropertyChanged();
                }
            }
        }

        public IEasingFunction RuningEasingFunction { get; }

        private UIElement _configUi;

        public UIElement ConfigUi
        {
            get { return _configUi; }
            private set { Set(ref _configUi, value); }
        }

        public bool IsNew { get; }
        public void Config()
        {
            throw new NotImplementedException();
        }

        public void Confirm()
        {
            throw new NotImplementedException();
        }

        public void SetEasingFunctionType(Type easingFunctionType)
        {
            if (ConfigEasingFunction == null)
            {
                ConfigEasingFunction = (IEasingFunction)Activator.CreateInstance(easingFunctionType);
            }
            else if(ConfigEasingFunction.GetType() != easingFunctionType)
            {
                ConfigEasingFunction = (IEasingFunction)Activator.CreateInstance(easingFunctionType);
            }
        }
    }
}
