using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Animation;
using EasingFunctionDemo.EasingFunctionConfigs;
using GalaSoft.MvvmLight;

namespace EasingFunctionDemo
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _animationSeconds = 4;

        /// <summary>
        /// 动画时长，默认4s
        /// </summary>
        public int AnimationSeconds
        {
            get { return _animationSeconds; }
            set { Set(ref _animationSeconds, value); }
        }

        private EasingFunctionConfig _config;

        /// <summary>
        /// 动画配置
        /// </summary>
        public EasingFunctionConfig Config
        {
            get { return _config; }
            set { Set(ref _config, value); }
        }

        private ObservableCollection<EasingFunctionConfig> _runningConfigs =
            new ObservableCollection<EasingFunctionConfig>();

        /// <summary>
        /// 运行中的动画配置
        /// </summary>
        public ObservableCollection<EasingFunctionConfig> RunningConfigs
        {
            get { return _runningConfigs; }
            set { Set(ref _runningConfigs, value); }
        }

        private List<Type> _easingFunctionTypes;

        /// <summary>
        /// 缓动函数类型
        /// </summary>
        public List<Type> EasingFunctionTypes
        {
            get { return _easingFunctionTypes; }
            set { Set(ref _easingFunctionTypes, value); }
        }

        private Type _selectedEasingFunctionType;

        /// <summary>
        /// 选中的缓动函数类型
        /// </summary>
        public Type SelectedEasingFunctionType
        {
            get { return _selectedEasingFunctionType; }
            set { Set(ref _selectedEasingFunctionType, value); }
        }

        public MainWindowViewModel()
        {
            EasingFunctionTypes = Assembly.GetAssembly(typeof(IEasingFunction)).GetTypes()
                .Where(s => typeof(IEasingFunction).IsAssignableFrom(s) &&
                            !(s == typeof(IEasingFunction) || s == typeof(EasingFunctionBase))).ToList();
        }
    }
}
