using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content.PM;
using XamarinFormsAnalyticsWrapper.Droid.Services;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;
using HockeyApp.Android.Utils;
using Acr.UserDialogs;
using ReactiveUI;

namespace WpApp.Droid
{
    [Activity(Label = "WpApp",
        Theme = "@style/MySplash",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        public MainActivity()
        {
            RxApp.SuspensionHost.CreateNewAppState = () => new Bootstrap();
            RxApp.SuspensionHost.SetupDefaultSuspendResume();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.MyTheme);

#if DEBUG
            HockeyLog.LogLevel = 3;
#endif
            CrashManager.Register(this, "5355b02ee8954d85b6c413df5afa72e0");
            MetricsManager.Register(this, Application, "5355b02ee8954d85b6c413df5afa72e0");

            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);
            UserDialogs.Init(() => (Activity)Forms.Context);
            var gaService = AnalyticsService.GetGASInstance();
            gaService.Init("UA-87598000-1", this, 5);

            var bootstrap = RxApp.SuspensionHost.GetAppState<Bootstrap>();
            var app = bootstrap.InitApp();

            LoadApplication(app);
        }
    }
}
