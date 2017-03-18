using System;
using SimpleInjector;
using WpApp.Helpers.Injector;

namespace WpApp
{
    public class Bootstrap
    {
        private Container container;

        public Bootstrap()
        {
            initSimpleInjector();
        }

        public App InitApp()
        {
            return container.GetInstance<App>();
        }

        private void initSimpleInjector()
        {
            container = new Container();

            var bootstraper = new SimpleInjectorBootstraper(container);
            bootstraper.Register();
            ServiceLocator.Container = container;
        }
    }
}
