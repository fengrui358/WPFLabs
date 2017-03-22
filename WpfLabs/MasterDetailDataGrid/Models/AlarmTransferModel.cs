using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL_Model;

namespace WpfLabs.MasterDetailDataGrid.Models
{
    public class AlarmTransferModel
    {
        /// <summary>
        /// 请求人Id
        /// </summary>
        public long RequestPersonId { get; set; }

        /// <summary>
        /// 请求人员名字
        /// </summary>
        public string RequestPersonName { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 请求人Id
        /// </summary>
        public long ResponsePersonId { get; set; }

        /// <summary>
        /// 请求人员名字
        /// </summary>
        public string ResponsePersonName { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime ResponseTime { get; set; }

        /// <summary>
        /// 审批结果
        /// </summary>
        public string ExaminationResult { get; set; }

        /// <summary>
        /// 报警信息
        /// </summary>
        public AlarmFullInfoModel AlarmFullInfoModel { get; set; }

        /// <summary>
        /// 合并的报警信息
        /// </summary>
        public IEnumerable<AlarmFullInfoModel> MergeAlarmFullInfoModels { get; set; }
    }
}
