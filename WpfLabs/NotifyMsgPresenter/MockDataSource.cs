using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfLabs.NotifyMsgPresenter
{
    /// <summary>
    /// 获取通知公告的模拟数据源
    /// </summary>
    public class MockDataSource : IDisposable
    {
        private int _mockIndex = 1;

        private int _intervalSecond = 3;
        private System.Threading.Timer _timer;

        /// <summary>
        /// 新消息到达
        /// </summary>
        public event Action<NotifyMsgModel> NewMsgReached;

        public MockDataSource(int intervalSecond = 3)
        {
            _intervalSecond = intervalSecond > 1 ? intervalSecond : 3;
            _timer = new System.Threading.Timer(NotifyNewMsg, null, TimeSpan.FromSeconds(_intervalSecond), TimeSpan.FromSeconds(_intervalSecond));
        }

        private void NotifyNewMsg(object obj)
        {
            if (NewMsgReached != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    var newNotifyMsgModel = new NotifyMsgModel
                    {
                        Id = _mockIndex,
                        Title = string.Format("第{0}条消息，{1} ", _mockIndex, DateTime.Now) + Guid.NewGuid() + Guid.NewGuid(),
                        Content = Guid.NewGuid().ToString("N")
                    };

                    _mockIndex++;
                    NewMsgReached(newNotifyMsgModel);
                }
            }
        }

        public void Dispose()
        {
            if (_timer != null) _timer.Dispose();
        }
    }
}
