using System;
using SimpleInjector;

namespace WpApp
{
    public static class ServiceLocator
    {
        public static Container Container { private get; set; }

        /// <summary>
        /// The service locator is reserved for special cases.
        /// </summary>
        public static TService GetInstance<TService>() where TService : class
        {
            return Container.GetInstance<TService>();
        }
    }
}
