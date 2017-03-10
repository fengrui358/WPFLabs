using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLabs.NotifyMsgPresenter
{
    /// <summary>
    /// 消息通知公告
    /// </summary>
    public class NotifyMsgModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
