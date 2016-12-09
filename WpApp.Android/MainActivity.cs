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
using Android.Graphics.Drawables;
using ImageCircle.Forms.Plugin.Droid;
using XamarinFormsAnalyticsWrapper.Droid.Services;
using Acr.UserDialogs;

namespace WpApp.Droid
{
    [Activity(Label = "WpApp",
        Theme = "@style/MySplash",
        MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.MyTheme);

            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;

            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            UserDialogs.Init(() => (Activity)Forms.Context);

            var gaService = AnalyticsService.GetGASInstance();
            gaService.Init("UA-87598000-1", this, 5);

            LoadApplication(new App());
        }
    }
}
