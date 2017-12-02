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
                    var ui = ConfigUi as EasingFunctionConfigUi;
                    if (ui != null)
                    {
                        ui.ConfigEasingFunctionChanged -=
                            OnConfigEasingFunctionChanged;
                    }

                    var newEasingFunctionConfigUi =
                        new EasingFunctionConfigUi((EasingFunctionBase) _configEasingFunction);
                    newEasingFunctionConfigUi.ConfigEasingFunctionChanged += OnConfigEasingFunctionChanged;
                    ConfigUi = newEasingFunctionConfigUi;
                    RaisePropertyChanged();
                }
            }
        }

        public event EventHandler ConfigEasingFunctionChanged;

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

        private void OnConfigEasingFunctionChanged(object sender, EventArgs args)
        {
            ConfigEasingFunctionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
