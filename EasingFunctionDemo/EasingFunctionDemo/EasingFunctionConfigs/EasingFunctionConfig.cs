using System;
using System.CodeDom;
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
            get => _configEasingFunction;
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

                    if (_configEasingFunction != null)
                    {
                        SplineKeyFrameConfig = null;

                        var newEasingFunctionConfigUi =
                            new EasingFunctionConfigUi((EasingFunctionBase)_configEasingFunction);
                        newEasingFunctionConfigUi.ConfigEasingFunctionChanged += OnConfigEasingFunctionChanged;
                        ConfigUi = newEasingFunctionConfigUi;
                    }
                    
                    RaisePropertyChanged();
                }
            }
        }

        private SplineKeyFrameConfigModel _splineKeyFrameConfig;

        public SplineKeyFrameConfigModel SplineKeyFrameConfig
        {
            get => _splineKeyFrameConfig;
            private set
            {
                if (_splineKeyFrameConfig != value)
                {
                    _splineKeyFrameConfig = value;
                    var ui = ConfigUi as SplineKeyFrameConfigUi;
                    if (ui != null)
                    {
                        ui.ConfigEasingFunctionChanged -=
                            OnConfigEasingFunctionChanged;
                    }

                    if (_splineKeyFrameConfig != null)
                    {
                        ConfigEasingFunction = null;

                        var newUi = new SplineKeyFrameConfigUi(_splineKeyFrameConfig);
                        newUi.ConfigEasingFunctionChanged += OnConfigEasingFunctionChanged;
                        ConfigUi = newUi;
                    }

                    RaisePropertyChanged();
                }
            }
        }

        public event EventHandler ConfigEasingFunctionChanged;

        private UIElement _configUi;

        public UIElement ConfigUi
        {
            get { return _configUi; }
            private set { Set(ref _configUi, value); }
        }

        public void SetEasingFunctionType(Type easingFunctionType)
        {
            if (easingFunctionType == null)
            {
                ConfigEasingFunction = null;
                return;
            }

            if (easingFunctionType == typeof(SplineKeyFrameConfigModel))
            {
                SplineKeyFrameConfig = new SplineKeyFrameConfigModel();
            }
            else
            {
                if (ConfigEasingFunction == null)
                {
                    ConfigEasingFunction = (IEasingFunction)Activator.CreateInstance(easingFunctionType);
                }
                else if (ConfigEasingFunction.GetType() != easingFunctionType)
                {
                    ConfigEasingFunction = (IEasingFunction)Activator.CreateInstance(easingFunctionType);
                }
            }            
        }

        public bool IsSplineKeyFrame => SplineKeyFrameConfig != null;

        private void OnConfigEasingFunctionChanged(object sender, EventArgs args)
        {
            ConfigEasingFunctionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
