using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp;

namespace WpfCefSharpDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _url;

        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            Browser.RenderProcessMessageHandler = new RenderProcessMessageHandler();

            //Url = "http://10.15.9.113:81/appeal-analysis?lang=zh-CN";
            //Url = "http://10.15.9.113:81/appeal-analysis?lang=en";
            //Url = "https://localhost:44399/";
            Url = "https://www.echartsjs.com/zh/index.html";

            //Task.Run(async () =>
            //{
            //    await Task.Delay(5000);
            //    Url = "http://10.15.9.113:81/appeal-analysis?lang=zh-CN";
            //});

            //Wait for the page to finish loading (all resources will have been loaded, rendering is likely still happening)
            Browser.LoadingStateChanged += (sender, args) =>
            {
                //Wait for the Page to finish loading
                if (args.IsLoading == false)
                {
                    //Browser.
                    //Browser.ExecuteJavaScriptAsync("alert('All Resources Have Loaded');");
                }
            };

            //Wait for the MainFrame to finish loading
            Browser.FrameLoadEnd += async (sender, args) =>
            {
                //Wait for the MainFrame to finish loading
                if (args.Frame.IsMain)
                {
                    const string scriptWithReturn = @"
                                testBytes('测试返回');";
                    var task = args.Frame.EvaluateScriptAsync(scriptWithReturn);
                    await task.ContinueWith(t =>
                    {
                        if (!t.IsFaulted)
                        {
                            var response = t.Result;
                            var x = response.Success ? (response.Result ?? "null") : response.Message;

                            MessageBox.Show(x.ToString());
                        }
                    });
                }
            };

            Browser.JavascriptObjectRepository.ResolveObject += (sender, e) =>
            {
                var repo = e.ObjectRepository;
                if (e.ObjectName == "boundAsync")
                {
                    //Binding options is an optional param, defaults to null
                    var bindingOptions =
                        BindingOptions
                            .DefaultBinder; //Use the default binder to serialize values into complex objects, CamelCaseJavascriptNames = true is the default

                    bindingOptions = new BindingOptions
                    {
                        CamelCaseJavascriptNames = true
                    }; //No camelcase of names and specify a default binder

                    //不使用bindingOptions，js端还是使用小写的命名方式
                    repo.Register("boundAsync", new BoundObject(), isAsync: true);
                }
            };

            Browser.JavascriptObjectRepository.ObjectBoundInJavascript += (sender, e) =>
            {
                var name = e.ObjectName;

                Debug.WriteLine($"Object {e.ObjectName} was bound successfully.");
            };

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}