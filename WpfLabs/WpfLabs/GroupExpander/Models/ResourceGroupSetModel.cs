using System;

namespace WpfLabs.GroupExpander.Models
{
    public class ResourceGroupSetModel
    {
        /// <summary>
        /// 车台/手台id
        /// </summary>
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 车台/手台电话
        /// </summary>
        public string Tel { get; set; }
    }
}
