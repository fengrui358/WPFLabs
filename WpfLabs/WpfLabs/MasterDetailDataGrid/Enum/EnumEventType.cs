namespace BOL_Model.Enum
{
    /// <summary>
    /// 事件类型枚举。
    /// </summary>
    public enum EnumEventType
    {
        /// <summary>
        /// 类型未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 警察
        /// </summary>
        Police,
        /// <summary>
        /// 交通
        /// </summary>
        Traffic,
        /// <summary>
        /// 消防
        /// </summary>
        FireControl,
        /// <summary>
        /// 医疗
        /// </summary>
        Medical,
    }

    /// <summary>
    /// 缓冲类型枚举。
    /// </summary>
    public enum EnumEventType_Buffer
    {
        /// <summary>
        /// 点。
        /// </summary>
        Spot,

        /// <summary>
        /// 线
        /// </summary>
        Line,

        /// <summary>
        /// 面
        /// </summary>
        Surface
    }
}
