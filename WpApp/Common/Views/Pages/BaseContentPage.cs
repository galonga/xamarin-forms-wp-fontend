using System;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using WpApp.Common.Extensions;
using Xamarin.Forms;

namespace WpApp.Common.Views.Pages
{
    public class BaseContentPage : ContentPage, IActivatable
    {
        public static BindableProperty AppearingCommandProperty = BindableProperty.Create(
            propertyName: nameof(AppearingCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(BaseContentPage),
            defaultValue: null
        );

        public static BindableProperty DisappearingCommandProperty = BindableProperty.Create(
            propertyName: nameof(DisappearingCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(BaseContentPage),
            defaultValue: null
        );

        public ICommand AppearingCommand {
            get { return (ICommand)GetValue(AppearingCommandProperty); }
            set { SetValue(AppearingCommandProperty, value); }
        }

        public ICommand DisappearingCommand {
            get { return (ICommand)GetValue(DisappearingCommandProperty); }
            set { SetValue(DisappearingCommandProperty, value); }
        }

        protected IObservable<EventPattern<EventArgs>> Deactivated;

        public BaseContentPage()
        {
            Deactivated = Observable.FromEventPattern<EventHandler, EventArgs>(
                h => this.Disappearing += h,
                h => this.Disappearing -= h
            );
            Deactivated.Subscribe(_ => DisappearingCommand.Raise());

            Locator.CurrentMutable.RegisterConstant(new ActivationForViewFetcher(), typeof(IActivationForViewFetcher));

            this.WhenActivated(d => {
                AppearingCommand.Raise();

                d(
                    Deactivated.Subscribe(_ => {
                        DisappearingCommand.Raise();
                    })
                );
            });
        }

        public BaseContentPage(INotifyPropertyChanged viewModel)
        {
            BindingContext = viewModel;
        }
    }
}
