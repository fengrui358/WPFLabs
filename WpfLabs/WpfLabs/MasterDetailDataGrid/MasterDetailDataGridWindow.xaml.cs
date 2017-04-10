using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BOL_Model;
using BOL_Model.Enum;
using WpfLabs.MasterDetailDataGrid.Models;

namespace WpfLabs.MasterDetailDataGrid
{
    /// <summary>
    /// MasterDetailDataGridWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MasterDetailDataGridWindow : Window
    {
        private static Random _random = new Random();

        private List<AlarmTransferModel> _alarmTransferModel;

        public List<AlarmTransferModel> AlarmTransferModel
        {
            get { return _alarmTransferModel; }
            set { _alarmTransferModel = value; }
        }

        public MasterDetailDataGridWindow()
        {
            InitializeComponent();

            DataContext = this;
            InitSource();
        }

        private void InitSource()
        {
            var datas = new List<AlarmTransferModel>();

            for (int i = 0; i < 20; i++)
            {
                var masterModel = GetNewModel();

                var children = new List<AlarmFullInfoModel>();

                for (int j = 0; j < 20; j++)
                {
                    children.Add(GetAlarmInfoModel());
                }

                masterModel.MergeAlarmFullInfoModels = children;

                datas.Add(masterModel);
            }

            AlarmTransferModel = datas;
        }

        private AlarmTransferModel GetNewModel()
        {
            var reslut = new AlarmTransferModel
            {
                ExaminationResult = Guid.NewGuid().ToString(),
                RequestPersonId = _random.Next(1, 100),
                RequestPersonName = Guid.NewGuid().ToString(),
                RequestTime = DateTime.Now.Add(TimeSpan.FromDays(_random.Next(1, 45))),
                ResponseTime = DateTime.Now.Add(TimeSpan.FromDays(_random.Next(1, 45))),
                ResponsePersonId = _random.Next(1, 100),
                ResponsePersonName = Guid.NewGuid().ToString()
            };

            var alarmInfo = GetAlarmInfoModel();

            reslut.AlarmFullInfoModel = alarmInfo;

            return reslut;
        }

        private AlarmFullInfoModel GetAlarmInfoModel()
        {
            var result = new AlarmFullInfoModel
            {
                MajorInfo = new AlarmMajorInfoModel
                {
                    EventGrade = (EnumEventGrade)_random.Next(1, 4),
                    EventType = (EnumEventType)_random.Next(1, 5),
                    IsDisposed = Convert.ToBoolean(_random.Next(0, 2))
                }
            };

            return result;
        }
    }
}