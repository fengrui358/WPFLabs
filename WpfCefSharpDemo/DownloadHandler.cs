using System;
using System.IO;
using CefSharp;

namespace WpfCefSharpDemo
{
    public class DownloadHandler : IDownloadHandler
    {
        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, downloadItem.SuggestedFileName);

            callback.Continue(path, true);
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            
        }
    }
}
