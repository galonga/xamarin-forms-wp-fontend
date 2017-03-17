using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ImageCircle.Forms.Plugin.iOS;
using XamarinFormsAnalyticsWrapper.iOS.Services;
using HockeyApp.iOS;

namespace WpApp.iOS
{
    [Foundation.Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(43, 132, 211); //bar background
            UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes() {
                Font = UIFont.FromName("HelveticaNeue-Light", (nfloat)20f),
                TextColor = UIColor.White
            });

            var manager = BITHockeyManager.SharedHockeyManager;
#if DEBUG
            manager.DebugLogEnabled = true;
#endif
            manager.Configure("4ff5552b508c4d6da01433b3fad81730");
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation();

            Forms.Init();

            var gaService = AnalyticsService.GetGASInstance();
            gaService.Init("UA-87598000-1", 5);

            ImageCircleRenderer.Init();

            LoadApplication(new App());


            return base.FinishedLaunching(app, options);
        }
    }
}
