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
        private int _intervalSecond = 3;
        private System.Threading.Timer _timer;
        private Random _random = new Random();

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
                var newNotifyMsgModel = new NotifyMsgModel
                {
                    Id = _random.Next(1, 1000),
                    Title = Guid.NewGuid().ToString("N"),
                    Content = Guid.NewGuid().ToString("N")
                };

                NewMsgReached(newNotifyMsgModel);
            }
        }

        public void Dispose()
        {
            if (_timer != null) _timer.Dispose();
        }
    }
}
