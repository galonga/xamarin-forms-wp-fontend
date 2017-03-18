using System;
using SimpleInjector;

namespace WpApp.Common.Extensions
{
    public static class SimpleInjectorExtensions
    {
        public static void RegisterLazy<T>(this Container container) where T : class
        {
            Func<T> factory = () => container.GetInstance<T>();

            container.Register<Lazy<T>>(() => new Lazy<T>(factory));
        }

        public static void RegisterFactory<T>(this Container container) where T : class
        {
            container.RegisterSingleton<Func<T>>(() => container.GetInstance<T>());
        }

        public static void RegisterServiceFactory<TService, TConcrete>(this Container container) where TService : class where TConcrete : class
        {
            container.Register(typeof(TService), typeof(TConcrete));
            container.RegisterSingleton<Func<TService>>(() => container.GetInstance<TService>());
        }
    }
}
