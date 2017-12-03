using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Animation;
using EasingFunctionDemo.EasingFunctionConfigs;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace EasingFunctionDemo
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _animationSeconds = ConstData.TotalMilliSeconds;

        /// <summary>
        /// 动画时长
        /// </summary>
        public int AnimationSeconds
        {
            get => _animationSeconds;
            set => Set(ref _animationSeconds, value);
        }

        private EasingFunctionConfig _config;

        /// <summary>
        /// 动画配置
        /// </summary>
        public EasingFunctionConfig Config
        {
            get => _config;
            set => Set(ref _config, value);
        }

        private ObservableCollection<EasingFunctionConfig> _runningConfigs =
            new ObservableCollection<EasingFunctionConfig>();

        /// <summary>
        /// 运行中的动画配置
        /// </summary>
        public ObservableCollection<EasingFunctionConfig> RunningConfigs
        {
            get => _runningConfigs;
            set => Set(ref _runningConfigs, value);
        }

        private ObservableCollection<Type> _easingFunctionTypes;

        /// <summary>
        /// 缓动函数类型
        /// </summary>
        public ObservableCollection<Type> EasingFunctionTypes
        {
            get => _easingFunctionTypes;
            set => Set(ref _easingFunctionTypes, value);
        }

        private Type _selectedEasingFunctionType;

        /// <summary>
        /// 选中的缓动函数类型
        /// </summary>
        public Type SelectedEasingFunctionType
        {
            get => _selectedEasingFunctionType;
            set
            {
                if (_selectedEasingFunctionType == value)
                {
                    return;
                }

                if (Config == null)
                {
                    Config = new EasingFunctionConfig();
                }

                _selectedEasingFunctionType = value;
                Config.SetEasingFunctionType(_selectedEasingFunctionType);

                RaisePropertyChanged();
            }
        }

        public RelayCommand AddNewEasingFunctionCommand { get; }

        public MainWindowViewModel()
        {
            EasingFunctionTypes = new ObservableCollection<Type>(Assembly.GetAssembly(typeof(IEasingFunction))
                .GetTypes()
                .Where(s => typeof(IEasingFunction).IsAssignableFrom(s) &&
                            !(s == typeof(IEasingFunction) || s == typeof(EasingFunctionBase))).ToList())
            {
                typeof(ReverseEase)
            };
            
            AddNewEasingFunctionCommand = new RelayCommand(AddNewEasingFunction);
        }

        /// <summary>
        /// 添加一个新的缓动函数
        /// </summary>
        private void AddNewEasingFunction()
        {
            SelectedEasingFunctionType = null;
            Config = new EasingFunctionConfig();
            SelectedEasingFunctionType = EasingFunctionTypes.First();
        }
    }
}
