using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using CefSharp;

namespace WpfCefSharpDemo
{
    public class RenderProcessMessageHandler : IRenderProcessMessageHandler
    {
        // Wait for the underlying `Javascript Context` to be created, this is only called for the main frame.
        // If the page has no javascript, no context will be created.
        async void IRenderProcessMessageHandler.OnContextCreated(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            const string script = @"
                        document.addEventListener('DOMContentLoaded', function()
                        { 
                            alert('DomLoaded');
                            //test('你好吗？');
                        });
                        ";

            frame.ExecuteJavaScriptAsync(script);
            //await frame.EvaluateScriptAsync(script);

            //const string scriptWithReturn = @"
            //            test('测试返回');";
            //var task = frame.EvaluateScriptAsync(scriptWithReturn);
            //await task.ContinueWith(async t =>
            //{
            //    if (!t.IsFaulted)
            //    {
            //        await Task.Delay(5000);

            //        var response = t.Result;
            //        var x = response.Success ? (response.Result ?? "null") : response.Message;

            //        MessageBox.Show(x.ToString());
            //    }
            //});
        }

        public void OnContextReleased(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
        {
            //throw new NotImplementedException();
        }

        public void OnFocusedNodeChanged(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IDomNode node)
        {
            //throw new NotImplementedException();
        }

        public void OnUncaughtException(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame,
            JavascriptException exception)
        {
            Debug.WriteLine(exception);
        }
    }
}