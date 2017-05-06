using Android.App;
using Android.OS;
using Android.Webkit;

namespace LiveLessons
{
    [Activity (Label = "LiveLessons", MainLauncher = true)]
    public class OpenWebPageActivity : Activity
    {
        private const string Host = "http://192.168.1.102:3000";

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

            var localWebView = FindViewById<WebView>(Resource.Id.LocalWebView);
            localWebView.SetWebViewClient(new WebViewClient());
            localWebView.Settings.JavaScriptEnabled = true;
            localWebView.LoadUrl(Host);
        }
    }
}


