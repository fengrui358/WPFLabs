using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 警情全数据
    /// </summary>
    public class AlarmFullInfoModel:ObservableObject
    {
        private AlarmBaseInfoModel _baseInfo;
        /// <summary>
        /// 基础信息 三元数据
        /// </summary>
        public AlarmBaseInfoModel BaseInfo
        {
            get { return _baseInfo; }
            set { Set(ref _baseInfo, value); }
        }
        
        private AlarmMajorInfoModel _majorInfo;
        /// <summary>
        /// 主要信息
        /// </summary>
        public AlarmMajorInfoModel MajorInfo
        {
            get { return _majorInfo; }
            set { Set(ref _majorInfo, value); }
        }

        private AlarmExtraInfoModel _extraInfo;
        /// <summary>
        /// 扩展信息
        /// </summary>
        public AlarmExtraInfoModel ExtraInfo
        {
            get { return _extraInfo; }
            set { Set(ref _extraInfo, value); }
        }
        
    }
}