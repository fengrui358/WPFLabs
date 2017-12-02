using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace EasingFunctionDemo.EasingFunctionConfigs
{
    public interface IEasingFunctionConfig
    {
        /// <summary>
        /// 用于配置的缓动函数
        /// </summary>
        IEasingFunction ConfigEasingFunction { get; }

        /// <summary>
        /// 用于运行的缓动函数
        /// </summary>
        IEasingFunction RuningEasingFunction { get; }

        /// <summary>
        /// 配置的Ui
        /// </summary>
        UIElement ConfigUi { get; }

        /// <summary>
        /// 是否是新创建
        /// </summary>
        bool IsNew { get; }

        /// <summary>
        /// 配置
        /// </summary>
        void Config();

        /// <summary>
        /// 配置确认
        /// </summary>
        void Confirm();

        /// <summary>
        /// 设置缓动函数类型
        /// </summary>
        /// <param name="easingFunctionType"></param>
        void SetEasingFunctionType(Type easingFunctionType);
    }
}
