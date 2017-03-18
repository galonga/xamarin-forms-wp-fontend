using System;
using SimpleInjector;
using WpApp.Common.Extensions;
using WpApp.Helpers.Tracking;
using WpApp.Services;
using WpApp.ViewModels;

namespace WpApp.Helpers.Injector
{
    public class SimpleInjectorBootstraper
    {
        readonly Container container;
        static Injectables Injectables { set; get; }

        public SimpleInjectorBootstraper(Container container)
        {
            this.container = container;
            SimpleInjectorBootstraper.Injectables = new Injectables(container);
        }

        public void Register()
        {
            registerTransients();
            registerSingletons();
            registerServiceFactories();
            registerExternals();
        }

        private void registerTransients()
        {
            container.Register<IWordpressService, WordpressService>();
        }

        private void registerSingletons()
        {
            //container.RegisterSingleton<IAppTracker, AppTracker>();
        }

        private void registerServiceFactories()
        {
            container.RegisterServiceFactory<IBlogFeedViewModel, BlogFeedViewModel>();
            container.RegisterServiceFactory<IPodcastViewModel, PodcastViewModel>();
            container.RegisterServiceFactory<ITwitterViewModel, TwitterViewModel>();
        }

        private void registerExternals()
        {
            //container.RegisterSingleton(() => Injectables.Config());
            container.RegisterSingleton(() => Injectables.GetGaService());
            container.RegisterSingleton(() => Injectables.GetUserDialog());
            container.RegisterSingleton(() => Injectables.GetAppTracker());
        }
    }
}
