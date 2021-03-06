﻿using Plugin.Share;
using System.Linq;
using Xamarin.Forms;
using WpApp.ViewsModels;
using WpApp.Models;
using WpApp.Helpers;

namespace WpApp.Views
{
    public partial class TwitterPage : ContentPage
    {
        private TwitterViewModel ViewModel {
            get { return BindingContext as TwitterViewModel; }
        }

        void OpenBrowser(string url)
        {
            CrossShare.Current.OpenBrowser(url, new Plugin.Share.Abstractions.BrowserOptions {
                ChromeShowTitle = true,
                ChromeToolbarColor = new Plugin.Share.Abstractions.ShareColor { R = 3, G = 169, B = 244, A = 255 },
                UseSafariWebViewController = true
            });
        }

        public TwitterPage()
        {
            InitializeComponent();

            BindingContext = new TwitterViewModel();

            listView.ItemTapped += (sender, args) => {
                if (listView.SelectedItem == null)
                    return;
                
                var tweet = listView.SelectedItem as Tweet;

                //try to launch twitter or tweetbot app, else launch browser
                var launch = DependencyService.Get<ILaunchTwitter>();
                if (launch == null || !launch.OpenStatus(tweet.StatusID.ToString()))
                    OpenBrowser("http://m.twitter.com/shanselman/status/" + tweet.StatusID);

                listView.SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || !ViewModel.CanLoadMore || ViewModel.IsBusy || ViewModel.Tweets.Count > 0)
                return;

            ViewModel.LoadTweetsCommand.Execute(null);
        }
    }
}
