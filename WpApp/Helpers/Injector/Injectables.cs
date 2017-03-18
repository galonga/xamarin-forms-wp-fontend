using System;
using Acr.UserDialogs;
using SimpleInjector;
using WpApp.Helpers.Tracking;
using Xamarin.Forms;
using XamarinFormsAnalyticsWrapper.Services;

namespace WpApp.Helpers.Injector
{
    public class Injectables
    {
        private readonly Container container;

        public Injectables(Container container)
        {
            this.container = container;
        }

        public IAnalyticsService GetGaService()
        {
            return DependencyService.Get<IAnalyticsService>();
        }

        public IUserDialogs GetUserDialog()
        {
            return UserDialogs.Instance;
        }

        public IAppTracker GetAppTracker()
        {
            var tracker = new AppTracker();

            var gaService = container.GetInstance<IAnalyticsService>();
            var analyticsTracker = new AnalyticsTracker(gaService);
            tracker.RegisterTracker(analyticsTracker);

            var errorTracker = new ErrorTracker();
            tracker.RegisterTracker(errorTracker);

            return tracker;
        }
    }
}
