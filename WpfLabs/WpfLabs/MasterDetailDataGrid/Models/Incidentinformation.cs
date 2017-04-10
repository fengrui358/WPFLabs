using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL_Model
{
    public class Incidentinformation : ObservableObject
    {

        private string _incidentInformationId;
        /// <summary>
        /// 警情ID
        /// </summary>
        public string incidentInformationId
        {
            get { return _incidentInformationId; }
            set
            {
                _incidentInformationId = value;
                RaisePropertyChanged(() => incidentInformationId);
            }
        }

        private double _incidentStateId;
        /// <summary>
        /// 警情状态ID
        /// </summary>
        public double incidentStateId
        {
            get { return _incidentStateId; }
            set
            {
                _incidentStateId = value;
                RaisePropertyChanged(() => incidentStateId);
            }
        }

        private double _incidentDisposalTypeId;
        /// <summary>
        /// 处警类型ID
        /// </summary>
        public double incidentDisposalTypeId
        {
            get { return _incidentDisposalTypeId; }
            set
            {
                _incidentDisposalTypeId = value;
                RaisePropertyChanged(() => incidentDisposalTypeId);
            }
        }

        private DateTime _startTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime startTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                RaisePropertyChanged(() => startTime);
            }
        }

        private DateTime _endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime endTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                RaisePropertyChanged(() => endTime);
            }
        }

        private string _incidentTitle;
        /// <summary>
        /// 警情标题
        /// </summary>
        public string incidentTitle
        {
            get { return _incidentTitle; }
            set
            {
                _incidentTitle = value;
                RaisePropertyChanged(() => incidentTitle);
            }
        }

        private double _latitude;
        /// <summary>
        /// 纬度
        /// </summary>
        public double latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                RaisePropertyChanged(() => latitude);
            }
        }

        private double _longitude;
        /// <summary>
        /// 经度
        /// </summary>
        public double longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                RaisePropertyChanged(() => longitude);
            }
        }

        private string _isReport;
        /// <summary>
        /// 是否上报
        /// </summary>
        public string isReport
        {
            get { return _isReport; }
            set
            {
                _isReport = value;
                RaisePropertyChanged(() => isReport);
            }
        }

        private string _incidentActuallyAddress;
        /// <summary>
        /// 警情实际发生地址
        /// </summary>
        public string incidentActuallyAddress
        {
            get { return _incidentActuallyAddress; }
            set
            {
                _incidentActuallyAddress = value;
                RaisePropertyChanged(() => incidentActuallyAddress);
            }
        }

        private string _incidentNumber;
        /// <summary>
        /// 警情用户可见编号
        /// </summary>
        public string incidentNumber
        {
            get { return _incidentNumber; }
            set
            {
                _incidentNumber = value;
                RaisePropertyChanged(() => incidentNumber);
            }
        }

        private double _invalidIncidentTypeId;
        /// <summary>
        /// 无效警情类型
        /// </summary>
        public double invalidIncidentTypeId
        {
            get { return _invalidIncidentTypeId; }
            set
            {
                _invalidIncidentTypeId = value;
                RaisePropertyChanged(() => invalidIncidentTypeId);
            }
        }
    }
}
