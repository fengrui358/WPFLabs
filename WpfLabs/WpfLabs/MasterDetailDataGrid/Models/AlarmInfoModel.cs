using System;
using System.Collections.Generic;
using BOL_Model.Enum;
using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 警情信息。
    /// </summary>
    public class AlarmInfoModel : ObservableObject
    {
        private EnumEventGrade _eventGrade;
        private EnumEventType _eventType;
        private string _reporteMan;
        private string _reportManAddress;
        private string _reportManPhone;
        private string _contactPhone;
        private string _incidentAddress;
        private double _incidentLng;
        private double _incidentLat;
        private string _incidentTitle;
        private string _incidentDescription;
        private DateTime _incidentTime;
        private DateTime _alarmTime;
        private string _province;
        private string _city;
        private bool _isApportion;
        private IncidentPoliceModel _incidentPolice;
        private IncidentTrafficModel _incidentTraffic;
        private IncidentFireControlModel _incidentFireControl;
        private IncidentMedicalModel _incidentMedical;

        /// <summary>
        /// 警情ID。
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// 警情等级。
        /// </summary>
        public EnumEventGrade EventGrade
        {
            get { return _eventGrade; }
            set { Set(ref _eventGrade, value); }
        }

        /// <summary>
        /// 警情类型。
        /// </summary>
        public EnumEventType EventType
        {
            get { return _eventType; }
            set { Set(ref _eventType, value); }
        }


        /// <summary>
        /// 席位ID。
        /// </summary>
        public int SeatInformationId { get; set; }

        /// <summary>
        /// 部门ID。
        /// </summary>
        public int OrgPersonId { get; set; }

        /// <summary>
        /// 报警人。
        /// </summary>
        public string ReporteMan
        {
            get { return _reporteMan; }
            set { Set(ref _reporteMan, value); }
        }

        /// <summary>
        /// 报警人地址。
        /// </summary>
        public string ReportManAddress
        {
            get { return _reportManAddress; }
            set { Set(ref _reportManAddress, value); }
        }

        /// <summary>
        /// 报警人电话。
        /// </summary>
        public string ReportManPhone
        {
            get { return _reportManPhone; }
            set { Set(ref _reportManPhone, value); }
        }

        /// <summary>
        /// 联系电话。
        /// </summary>
        public string ContactPhone
        {
            get { return _contactPhone; }
            set { Set(ref _contactPhone, value); }
        }

        /// <summary>
        /// 事发地址。
        /// </summary>
        public string IncidentAddress
        {
            get { return _incidentAddress; }
            set { Set(ref _incidentAddress, value); }
        }

        /// <summary>
        /// 事发点经度。
        /// </summary>
        public double IncidentLng
        {
            get { return _incidentLng; }
            set { Set(ref _incidentLng, value); }
        }

        /// <summary>
        /// 事发点纬度。
        /// </summary>
        public double IncidentLat
        {
            get { return _incidentLat; }
            set { Set(ref _incidentLat, value); }
        }

        /// <summary>
        /// 警情标题。
        /// </summary>
        public string IncidentTitle
        {
            get { return _incidentTitle; }
            set { Set(ref _incidentTitle, value); }
        }

        /// <summary>
        /// 警情说明。
        /// </summary>
        public string IncidentDescription
        {
            get { return _incidentDescription; }
            set { Set(ref _incidentDescription, value); }
        }

        /// <summary>
        /// 事发时间。
        /// </summary>
        public DateTime IncidentTime
        {
            get { return _incidentTime; }
            set { Set(ref _incidentTime, value); }
        }

        /// <summary>
        /// 接警时间。
        /// </summary>
        public DateTime AlarmTime
        {
            get { return _alarmTime; }
            set { Set(ref _alarmTime, value); }
        }

        /// <summary>
        /// 省。
        /// </summary>
        public string Province
        {
            get { return _province; }
            set { Set(ref _province, value); }
        }

        /// <summary>
        /// 市。
        /// </summary>
        public string City
        {
            get { return _city; }
            set { Set(ref _city, value); }
        }

        /// <summary>
        /// 是否分配。
        /// </summary>
        public bool IsApportion
        {
            get { return _isApportion; }
            set { Set(ref _isApportion, value); }
        }

        /// <summary>
        /// 原始短信内容。
        /// </summary>
        public string SmsContent { get; set; }

        /// <summary>
        /// 警察专业警情。
        /// </summary>
        public IncidentPoliceModel IncidentPolice
        {
            get { return _incidentPolice; }
            set { Set(ref _incidentPolice, value); }
        }

        /// <summary>
        /// 交通专业警情。
        /// </summary>
        public IncidentTrafficModel IncidentTraffic
        {
            get { return _incidentTraffic; }
            set { Set(ref _incidentTraffic, value); }
        }

        /// <summary>
        /// 消防专业警情。
        /// </summary>
        public IncidentFireControlModel IncidentFireControl
        {
            get { return _incidentFireControl; }
            set { Set(ref _incidentFireControl, value); }
        }

        /// <summary>
        /// 医疗专业警情。
        /// </summary>
        public IncidentMedicalModel IncidentMedical
        {
            get { return _incidentMedical; }
            set { Set(ref _incidentMedical, value); }
        }
    }
}