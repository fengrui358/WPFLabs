using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL_Model.Enum
{
    /// <summary>
    /// 警情等级枚举。
    /// </summary>
    public enum EnumEventGrade
    {
        ///<summary>
        /// 等级未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 一级，最低
        /// </summary>
        One,
        /// <summary>
        /// 二级，中等
        /// </summary>
        Two,
        /// <summary>
        /// 三级，严重
        /// </summary>
        Three
    }
}
