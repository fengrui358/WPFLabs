using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WpfLabs.BindingDemo
{
    public class BindingTargetClass : ObservableObject
    {
        private static readonly object AsyncObj = new object();
        private static BindingTargetClass _instance;
        private string _testString;

        public string TestString
        {
            get => _testString;
            set
            {
                _testString = value;
                RaisePropertyChanged();
            }
        }

        public static BindingTargetClass Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (AsyncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new BindingTargetClass();
                        }
                    }
                }

                return _instance;
            }
        }

        private BindingTargetClass()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    TestString = Guid.NewGuid().ToString();
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
