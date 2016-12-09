using System;
using Acr.UserDialogs;

namespace WpApp.Common.Extensions
{
    public static class LoadingExtension
    {
        public static void AttachLoading(this IObservable<bool> observable, string text = "Bitte warten")
        {
            observable.Subscribe(show => {
                var userDialogs = UserDialogs.Instance;
                if (show) {
                    userDialogs?.ShowLoading(text);
                } else {
                    userDialogs?.HideLoading();
                }
            });
        }
    }
}