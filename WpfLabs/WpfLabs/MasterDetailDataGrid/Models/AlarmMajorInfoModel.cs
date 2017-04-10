using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BOL_Model.Enum;
using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 警情主要信息
    /// </summary>
    public class AlarmMajorInfoModel : ObservableObject
    {

        private EnumEventType _eventType;
        /// <summary>
        /// 事件类型
        /// </summary>
        public EnumEventType EventType
        {
            get { return _eventType; }
            set
            {
                _eventType = value;
                RaisePropertyChanged(() => EventType);
            }
        }
        private EnumEventGrade _eventGrade;
        /// <summary>
        /// 事件等级
        /// </summary>
        public EnumEventGrade EventGrade
        {
            get { return _eventGrade; }
            set
            {
                _eventGrade = value;
                RaisePropertyChanged(() => EventGrade);
            }
        }
        private DateTime _eventTime;
        /// <summary>
        /// 事件时间
        /// </summary>
        public DateTime EventTime
        {
            get { return _eventTime; }
            set
            {
                _eventTime = value;
                RaisePropertyChanged(() => EventTime);
            }
        }
        private string _province;
        /// <summary>
        /// 省
        /// </summary>
        public string Province
        {
            get { return _province; }
            set
            {
                _province = value;
                RaisePropertyChanged(() => Province);
            }
        }
        private string _city;
        /// <summary>
        /// 市
        /// </summary>
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                RaisePropertyChanged(() => City);
            }
        }
        private string _discription;
        /// <summary>
        /// 描述
        /// </summary>
        public string Discription
        {
            get { return _discription; }
            set
            {
                _discription = value;
                RaisePropertyChanged(() => Discription);
            }
        }

        private Dictionary<string, ObservableCollection<string>> _provCityList;
        /// <summary>
        /// 省市列表
        /// </summary>
        public Dictionary<string, ObservableCollection<string>> ProvCityList
        {
            get { return _provCityList; }
            set
            {
                _provCityList = value;
                RaisePropertyChanged(() => ProvCityList);
            }
        }

        private bool _isDisposed;

        /// <summary>
        /// 是否已派遣
        /// </summary>
        public bool IsDisposed
        {
            get { return _isDisposed; }
            set { Set(ref _isDisposed, value); }
        }

    }
}
