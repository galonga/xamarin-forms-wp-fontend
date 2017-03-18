using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using LinqToTwitter;
using System.Threading.Tasks;
using System.Linq;
using WpApp.Models;
using WpApp.Helpers;
using XamarinFormsAnalyticsWrapper.Services;
using WpApp.Helpers.Tracking;

namespace WpApp.ViewModels
{
    public class TwitterViewModel : BaseViewModel, ITwitterViewModel
    {
        public ObservableCollection<Tweet> Tweets { get; set; }

        readonly IAppTracker tracker;

        public TwitterViewModel(IAppTracker tracker)
        {
            this.tracker = tracker;
            Title = "Twitter";
            Icon = "slideout.png";
            Tweets = new ObservableCollection<Tweet>();

            tracker.TrackScreen(Title);
        }

        private Command loadTweetsCommand;

        public Command LoadTweetsCommand {
            get {
                return loadTweetsCommand ??
                  (loadTweetsCommand = new Command(async () => {
                      await ExecuteLoadTweetsCommand();
                  }, () => {
                      return !IsBusy;
                  }));
            }
        }

        public async Task ExecuteLoadTweetsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            LoadTweetsCommand.ChangeCanExecute();
            var error = false;
            try {
                Tweets.Clear();
                var auth = new ApplicationOnlyAuthorizer() {
                    CredentialStore = new InMemoryCredentialStore {
                        ConsumerKey = "ZTmEODUCChOhLXO4lnUCEbH2I",
                        ConsumerSecret = "Y8z2Wouc5ckFb1a0wjUDT9KAI6DUat5tFNdmIkPLl8T4Nyaa2J",
                    },
                };
                await auth.AuthorizeAsync();

                var twitterContext = new TwitterContext(auth);

                var queryResponse = await
                  (from tweet in twitterContext.Status
                   where tweet.Type == StatusType.User &&
                     tweet.ScreenName == "shanselman" &&
                     tweet.Count == 100 &&
                     tweet.IncludeRetweets == true &&
                     tweet.ExcludeReplies == true
                   select tweet).ToListAsync();

                var tweets =
                  (from tweet in queryResponse
                   select new Tweet {
                       StatusID = tweet.StatusID,
                       ScreenName = tweet.User.ScreenNameResponse,
                       Text = tweet.Text,
                       CurrentUserRetweet = tweet.CurrentUserRetweet,
                       CreatedAt = tweet.CreatedAt,
                       Image = tweet.RetweetedStatus != null && tweet.RetweetedStatus.User != null ?
                                      tweet.RetweetedStatus.User.ProfileImageUrl.Replace("http://", "https://") : (tweet.User.ScreenNameResponse == "shanselman" ? "scott159.png" : tweet.User.ProfileImageUrl.Replace("http://", "https://"))
                   }).ToList();
                foreach (var tweet in tweets) {
                    Tweets.Add(tweet);
                }

                if (Device.OS == TargetPlatform.iOS) {
                    // only does anything on iOS, for the Watch
                    DependencyService.Get<ITweetStore>().Save(tweets);
                }
            } catch (Exception ex) {
                error = true;
                tracker.TrackException(ex);
            }

            if (error) {
                var page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load tweets.", "OK");
            }

            IsBusy = false;
            LoadTweetsCommand.ChangeCanExecute();
        }
    }
}

