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
        /// 样条关键帧动画配置
        /// </summary>
        SplineKeyFrame SplineKeyFrameConfig { get; }

        /// <summary>
        /// 配置发生变化
        /// </summary>
        event EventHandler ConfigEasingFunctionChanged;

        /// <summary>
        /// 配置的Ui
        /// </summary>
        UIElement ConfigUi { get; }

        /// <summary>
        /// 设置缓动函数类型
        /// </summary>
        /// <param name="easingFunctionType"></param>
        void SetEasingFunctionType(Type easingFunctionType);

        /// <summary>
        /// 是否是三次贝塞尔曲线控制的样条关键帧动画
        /// </summary>
        bool IsSplineKeyFrame { get; }
    }
}
