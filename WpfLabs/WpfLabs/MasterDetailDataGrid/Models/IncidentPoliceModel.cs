using BOL_Model.Enum;
using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 警察专业警情。
    /// </summary>
    public class IncidentPoliceModel : ObservableObject
    {
        /// <summary>
        /// 是否人员伤亡。
        /// </summary>
        public bool IsCasualty { get; set; }

        /// <summary>
        /// 受牵连人员数量。
        /// </summary>
        public int ImplicationNumber { get; set; }

        /// <summary>
        /// 可疑特征。
        /// </summary>
        public string ShadinessCharacter { get; set; }

        /// <summary>
        /// 概况。
        /// </summary>
        public string Summary { get; set; }
    }
}