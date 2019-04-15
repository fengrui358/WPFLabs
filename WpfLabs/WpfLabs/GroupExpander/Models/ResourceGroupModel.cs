using System;
using System.Collections.Generic;

namespace WpfLabs.GroupExpander.Models
{
    /// <summary>
    /// 资源组详细信息
    /// </summary>
    public class ResourceGroupModel
    {
        /// <summary>
        /// 组ID
        /// </summary>
        public Guid GroupId { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>

        public string GroupName { get; set; }

        /// <summary>
        /// 组标识
        /// </summary>
        public string GroupRemark { get; set; }

        /// <summary>
        /// 组中的车台手台
        /// </summary>
        public List<ResourceGroupSetModel> ResourceGroupSetInfos { get; set; }
    }
}
