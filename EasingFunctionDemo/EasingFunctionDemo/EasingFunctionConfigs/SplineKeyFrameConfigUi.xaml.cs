using System;

namespace EasingFunctionDemo.EasingFunctionConfigs
{
    /// <summary>
    /// SplineKeyFrameConfigUi.xaml 的交互逻辑
    /// </summary>
    public partial class SplineKeyFrameConfigUi
    {
        public SplineKeyFrame SplineKeyFrameConfigModel { get; }

        public event EventHandler ConfigEasingFunctionChanged;

        public SplineKeyFrameConfigUi(SplineKeyFrame splineKeyFrameConfigModel)
        {
            SplineKeyFrameConfigModel = splineKeyFrameConfigModel;
            InitializeComponent();

            DataContext = SplineKeyFrameConfigModel;
            SplineKeyFrameConfigModel.PropertyChanged +=
                (sender, args) => ConfigEasingFunctionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
