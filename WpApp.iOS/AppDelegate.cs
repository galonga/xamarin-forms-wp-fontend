using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinFormsAnalyticsWrapper.iOS.Services;
using HockeyApp.iOS;
using ReactiveUI;

namespace WpApp.iOS
{
    [Foundation.Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
        AutoSuspendHelper suspendHelper;

        public AppDelegate()
        {
            RxApp.SuspensionHost.CreateNewAppState = () => new Bootstrap();
        }

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

            var gaService = AnalyticsService.GetGASInstance();
            gaService.Init("UA-87598000-1", 5);

            Forms.Init();


            RxApp.SuspensionHost.SetupDefaultSuspendResume();
            suspendHelper = new AutoSuspendHelper(this);
            suspendHelper.FinishedLaunching(app, options);

            var bootstrap = RxApp.SuspensionHost.GetAppState<Bootstrap>();
            var application = bootstrap.InitApp();

            LoadApplication(application);

            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            suspendHelper.OnActivated(uiApplication);
            uiApplication.ApplicationIconBadgeNumber = 0;
        }

        public override void DidEnterBackground(UIApplication uiApplication)
        {
            base.DidEnterBackground(uiApplication);
            suspendHelper.DidEnterBackground(uiApplication);
        }
    }
}
