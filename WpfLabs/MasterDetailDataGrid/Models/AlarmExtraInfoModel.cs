using BOL_Model.Enum;
using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 警情补充信息
    /// </summary>
    public class AlarmExtraInfoModel:ObservableObject
    {

        private bool _isAnyInjury;
        /// <summary>
        /// 是否伤亡
        /// </summary>
        public bool IsAnyInjury
        {
            get { return _isAnyInjury; }
            set
            {
                _isAnyInjury = value;
                RaisePropertyChanged(() => IsAnyInjury);
            }
        }

        private EnumInjuryGrade _injuryGrade;
        /// <summary>
        /// 伤亡等级
        /// </summary>
        public EnumInjuryGrade InjuryGrade
        {
            get { return _injuryGrade; }
            set
            {
                _injuryGrade = value;
                RaisePropertyChanged(() => InjuryGrade);
            }
        }

        private bool _isNotify;
        /// <summary>
        /// 是否知会
        /// </summary>
        public bool IsNotify
        {
            get { return _isNotify; }
            set
            {
                _isNotify = value;
                RaisePropertyChanged(() => IsNotify);
            }
        }

        private string _description;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private string _suspiciousFeature;
        /// <summary>
        /// 可疑特征
        /// </summary>
        public string SuspiciousFeature
        {
            get { return _suspiciousFeature; }
            set
            {
                _suspiciousFeature = value;
                RaisePropertyChanged(() => SuspiciousFeature);
            }
        }

    }
}