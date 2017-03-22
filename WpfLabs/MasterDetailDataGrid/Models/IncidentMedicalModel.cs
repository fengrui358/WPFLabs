using BOL_Model.Enum;
using GalaSoft.MvvmLight;

namespace BOL_Model
{
    /// <summary>
    /// 医疗专业警情。
    /// </summary>
    public class IncidentMedicalModel : ObservableObject
    {
        /// <summary>
        /// 受伤者状态。
        /// </summary>
        public EnumInjuredStatus InjuredStatus { get; set; }

        /// <summary>
        /// 受伤者数量。
        /// </summary>
        public int InjuredNumber { get; set; }


        /// <summary>
        /// 是否有呼吸。
        /// </summary>
        public bool? HasBreathe { get; set; }

        /// <summary>
        /// 胸部是否受伤。
        /// </summary>
        public bool? IsBreastInjured { get; set; }

        /// <summary>
        /// 是否脸色发青。
        /// </summary>
        public bool? IsFaceBlack { get; set; }

        /// <summary>
        /// 是否流血过多。
        /// </summary>
        public bool? IsBleedingTooMuch { get; set; }

        /// <summary>
        /// 是否骨折。
        /// </summary>
        public bool? IsFracture { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Description { get; set; }
    }
}