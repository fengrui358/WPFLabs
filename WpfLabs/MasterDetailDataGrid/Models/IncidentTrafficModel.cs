using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 交通专业警情。
    /// </summary>
    public class IncidentTrafficModel : ObservableObject
    {
        /// <summary>
        /// 是否人员伤亡。
        /// </summary>
        public bool IsCasualty { get; set; }

        /// <summary>
        /// 事发地车辆数量。
        /// </summary>
        public int CarNumber { get; set; }

        /// <summary>
        /// 概况。
        /// </summary>
        public string Summary { get; set; }
    }
}