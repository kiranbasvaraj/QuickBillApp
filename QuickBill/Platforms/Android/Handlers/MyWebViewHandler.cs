using Microsoft.Maui.Handlers;
using AWebView = Android.Webkit.WebView;

namespace QuickBill.Platforms.Android
{
    public class MyWebViewHandler : WebViewHandler
    {
        protected override void ConnectHandler(AWebView platformView)
        {
            base.ConnectHandler(platformView);

            platformView.Settings.AllowFileAccess = true;
            platformView.Settings.AllowUniversalAccessFromFileURLs = true;
            platformView.Settings.AllowFileAccessFromFileURLs = true;
        }
    }
}
