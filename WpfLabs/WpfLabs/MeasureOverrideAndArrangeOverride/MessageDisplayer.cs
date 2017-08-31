using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WpfLabs.MeasureOverrideAndArrangeOverride
{
    public class MessageDisplayer : ObservableObject
    {
        private static readonly object AsyncObj = new object();
        private static MessageDisplayer _instance;
        private string _msg;

        public static MessageDisplayer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (AsyncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new MessageDisplayer();
                        }
                    }
                }

                return _instance;
            }
        }

        private MessageDisplayer()
        {
        }

        /// <summary>
        /// 界面展示信息
        /// </summary>
        public string Msg
        {
            get => _msg;
            private set => Set(ref _msg, value);
        }

        public void Clear()
        {
            Msg = string.Empty;
        }

        public void AppendLine(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                Msg = string.IsNullOrEmpty(Msg) ? s : string.Concat(Msg, Environment.NewLine, s);
            }
        }
    }
}
